using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudPersonasDiego.Data;
using CrudPersonasDiego.Models;

namespace CrudPersonasDiego.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonasApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            return await _context.PersonasDiegoN.ToListAsync();
        }

        // GET: api/PersonasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.PersonasDiegoN.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // GET: api/PersonasApi/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonasActivas()
        {
            return await _context.PersonasDiegoN.Where(p => p.Estatus == 1).ToListAsync();
        }

        // GET: api/PersonasApi/inactivos
        [HttpGet("inactivos")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonasInactivas()
        {
            return await _context.PersonasDiegoN.Where(p => p.Estatus == 0).ToListAsync();
        }

        // GET: api/PersonasApi/buscar/{nombre}
        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<IEnumerable<Persona>>> BuscarPersonas(string nombre)
        {
            var personas = await _context.PersonasDiegoN
                .Where(p => p.Nombres.Contains(nombre) ||
                           p.ApellidoP.Contains(nombre) ||
                           p.ApellidoM.Contains(nombre))
                .ToListAsync();

            return personas;
        }

        // PUT: api/PersonasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, Persona persona)
        {
            if (id != persona.Id)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PersonasApi
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(Persona persona)
        {
            persona.Estatus = 1; // Activo por defecto
            _context.PersonasDiegoN.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersona", new { id = persona.Id }, persona);
        }

        // DELETE: api/PersonasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.PersonasDiegoN.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            // En lugar de eliminar físicamente, cambiamos el estatus a inactivo
            persona.Estatus = 0;
            _context.Entry(persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PersonasApi/5/activar
        [HttpPost("{id}/activar")]
        public async Task<IActionResult> ActivarPersona(int id)
        {
            var persona = await _context.PersonasDiegoN.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            persona.Estatus = 1;
            _context.Entry(persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return _context.PersonasDiegoN.Any(e => e.Id == id);
        }
    }
}