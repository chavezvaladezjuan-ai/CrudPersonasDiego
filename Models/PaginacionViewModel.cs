using CrudPersonasDiego.Models;

namespace CrudPersonasDiego.Models
{
    public class PaginacionViewModel
    {
        public List<Persona> Personas { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalRegistros { get; set; }
        public int RegistrosPorPagina { get; set; }
        public string FiltroActual { get; set; }
        public string FiltroEstatus { get; set; }
    }

    public static class PaginacionExtensions
    {
        public static PaginacionViewModel ToPagedList(this IQueryable<Persona> source, int pagina, int pageSize, string filtroActual, string filtroEstatus)
        {
            var count = source.Count();
            var items = source.Skip((pagina - 1) * pageSize).Take(pageSize).ToList();

            return new PaginacionViewModel
            {
                Personas = items,
                PaginaActual = pagina,
                TotalRegistros = count,
                TotalPaginas = (int)Math.Ceiling(count / (double)pageSize),
                RegistrosPorPagina = pageSize,
                FiltroActual = filtroActual,
                FiltroEstatus = filtroEstatus
            };
        }
    }
}