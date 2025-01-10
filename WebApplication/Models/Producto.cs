using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        [Required]
        public decimal Precio { get; set; }

        [StringLength(500)]
        public string ImagenUrl { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [ForeignKey("CategoriaId")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}

