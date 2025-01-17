using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProductosController : Controller
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var productos = _context.Productos.ToList(); 
            return View(productos);
        }


        public IActionResult Details(int id)
        {
            // Datos ficticios
            var productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Mini Lota Mostaza", Precio = 49.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso práctico y elegante."  },
            new Producto { Id = 2, Nombre = "Mini Lota Azul", Precio = 59.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso azul con estilo clásico." },
            new Producto { Id = 3, Nombre = "Lota Escocés Azul", Precio = 69.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso con diseño escocés en tonos azules." },
            new Producto { Id = 4, Nombre = "Lota Escocés Verde", Precio = 79.99M, ImagenUrl = "Prueba.png" },

            new Producto { Id = 5, Nombre = "Mini Lota Mostaza", Precio = 49.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso práctico y elegante."  },
            new Producto { Id = 6, Nombre = "Mini Lota Azul", Precio = 59.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso azul con estilo clásico." },
            new Producto { Id = 7, Nombre = "Lota Escocés Azul", Precio = 69.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso con diseño escocés en tonos azules." },
            new Producto { Id = 8, Nombre = "Lota Escocés Verde", Precio = 79.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso escocés en tonos verdes." },

            new Producto { Id = 9, Nombre = "Mini Lota Mostaza", Precio = 49.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso práctico y elegante."  },
            new Producto { Id = 10, Nombre = "Mini Lota Azul", Precio = 59.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso azul con estilo clásico." },
            new Producto { Id = 11, Nombre = "Lota Escocés Azul", Precio = 69.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso con diseño escocés en tonos azules." },
            new Producto { Id = 12, Nombre = "Lota Escocés Verde", Precio = 79.99M, ImagenUrl = "Prueba.png", Descripcion = "Bolso escocés en tonos verdes." },
        };

            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        
    }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nombre,Descripcion,Precio,ImagenUrl,Stock,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);  // Agregar el producto
                _context.SaveChanges();  // Guardar cambios en la base de datos
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        public IActionResult Edit(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Descripcion,Precio,ImagenUrl,Stock,CategoriaId")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);  
                    _context.SaveChanges();  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);  
        }


        public IActionResult Delete(int id)
        {
            var producto = _context.Productos
                .FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var producto = _context.Productos.Find(id);
            _context.Productos.Remove(producto);  // Eliminar producto
            _context.SaveChanges();  // Guardar cambios
            return RedirectToAction(nameof(Index));
        }
    }
}
