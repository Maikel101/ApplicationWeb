using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProductoController : Controller
    {
        private readonly AppDbContext _context;

        public ProductoController(AppDbContext context)
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
            var producto = _context.Productos
                .FirstOrDefault(p => p.Id == id);

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
