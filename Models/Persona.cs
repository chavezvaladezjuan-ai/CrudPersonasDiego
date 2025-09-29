using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudPersonasDiego.Models
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombres")]
        public string? Nombres { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [Display(Name = "Apellido Paterno")]
        public string? ApellidoP { get; set; }

        [Required(ErrorMessage = "El apellido materno es obligatorio")]
        [Display(Name = "Apellido Materno")]
        public string? ApellidoM { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        public int Estatus { get; set; } = 1;

        [NotMapped]
        public string NombreCompleto => $"{Nombres} {ApellidoP} {ApellidoM}";
    }
}