using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(100, ErrorMessage = "El nombre completo no puede tener más de 100 caracteres.")]
        public string NombreCompleto { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }
}
