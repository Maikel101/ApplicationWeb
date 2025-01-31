using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }

        public string? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Required]
        public string SessionId { get; set; }

        [Required]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
