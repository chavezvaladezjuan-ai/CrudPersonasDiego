using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudPersonasDiego.Data;
using CrudPersonasDiego.Models;

namespace CrudPersonasDiego.Controllers
{
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personas
        // GET: Personas
        public async Task<IActionResult> Index(string searchString, string filtroEstatus, int pagina = 1, int registrosPorPagina = 10)
        {
            var personas = from p in _context.PersonasDiegoN
                           select p;

            // Aplicar filtro de búsqueda
            if (!string.IsNullOrEmpty(searchString))
            {
                personas = personas.Where(p =>
                    p.Nombres.Contains(searchString) ||
                    p.ApellidoP.Contains(searchString) ||
                    p.ApellidoM.Contains(searchString));
            }

            // Aplicar filtro de estatus
            if (!string.IsNullOrEmpty(filtroEstatus))
            {
                if (filtroEstatus == "Activos")
                {
                    personas = personas.Where(p => p.Estatus == 1);
                }
                else if (filtroEstatus == "Inactivos")
                {
                    personas = personas.Where(p => p.Estatus == 0);
                }
            }

            // Aplicar paginación
            var modeloPaginado = personas.ToPagedList(pagina, registrosPorPagina, searchString, filtroEstatus);

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentStatusFilter"] = filtroEstatus;
            ViewData["RegistrosPorPagina"] = registrosPorPagina;

            return View(modeloPaginado);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombres,ApellidoP,ApellidoM,Direccion,Telefono")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persona.Estatus = 1; // Activo por defecto
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.PersonasDiegoN.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,ApellidoP,ApellidoM,Direccion,Telefono,Estatus")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var persona = await _context.PersonasDiegoN.FindAsync(id);
            if (persona != null)
            {
                persona.Estatus = 0; // Cambiar a inactivo
                _context.Update(persona);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Personas/Activate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate(int id)
        {
            var persona = await _context.PersonasDiegoN.FindAsync(id);
            if (persona != null)
            {
                persona.Estatus = 1; // Cambiar a activo
                _context.Update(persona);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _context.PersonasDiegoN.Any(e => e.Id == id);
        }
    }
}