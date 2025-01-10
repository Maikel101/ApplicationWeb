using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class DetallePedido
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

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
