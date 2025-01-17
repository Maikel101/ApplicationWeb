using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        [StringLength(15, ErrorMessage = "El número de teléfono no puede tener más de 15 caracteres.")]

        public string Telefono { get; set; }

        [Required]
        [StringLength(500)]
        public string Mensaje { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
