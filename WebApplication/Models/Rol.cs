using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Rol : IdentityRole
    {
        [StringLength(50, ErrorMessage = "La descripción no puede exceder los 50 caracteres.")]
        public string Descripcion { get; set; }
    }
}
