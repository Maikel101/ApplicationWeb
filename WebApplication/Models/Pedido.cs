using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; } // Usuario que hizo el pedido
        public Usuario Usuario { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(200)]
        public string Direccion { get; set; }

        [Required]
        public decimal Total { get; set; }
        public ICollection<DetallePedido> Detalles { get; set; }
    }
}
