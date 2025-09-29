namespace CrudPersonasDiego.Models
{
    public class PersonaDto
    {
        public int Id { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public int Estatus { get; set; }
        public string? NombreCompleto { get; set; }
    }

    public class CreatePersonaDto
    {
        public string? Nombres { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}