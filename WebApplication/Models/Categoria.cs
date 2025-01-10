using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        public ICollection<Producto> Productos { get; set; } // Relación con Productos
    }
}
