using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using System.Linq;
using Newtonsoft.Json;

namespace WebApplication.Controllers
{
    public class CarritoController : Controller
    {

        private readonly AppDbContext _context;

        public CarritoController(AppDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar el carrito
        public IActionResult Index()
        {
            string sessionId = GetSessionId();
            var carritoItems = _context.Carritos
                .Include(c => c.Producto)
                .Where(c => c.SessionId == sessionId)
                .ToList();

            return View(carritoItems);
        }
        //public IActionResult Index()
        //{
        //    var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito");
        //    return View(carrito ?? new List<CarritoItem>());
        //}

        [HttpPost]
        public IActionResult Agregar(int productoId, int cantidad)
        {
            string sessionId = GetSessionId();  // Aquí obtenemos el SessionId.

            var producto = _context.Productos.Find(productoId);

            if (producto == null)
            {
                return NotFound();
            }

            // Determina si el usuario está autenticado y se usa el SessionId o UsuarioId.
            var usuarioId = User.Identity.IsAuthenticated ? User.Identity.Name : sessionId;  // Si el usuario está logueado, usar su Id.

            var carritoItem = _context.Carritos
                .FirstOrDefault(c => c.ProductoId == productoId && c.SessionId == sessionId);

            if (carritoItem != null)
            {
                // Si el producto ya está en el carrito, actualiza la cantidad
                carritoItem.Cantidad += cantidad;
            }
            else
            {
                // Si el producto no está en el carrito, agrégalo
                carritoItem = new Carrito
                {
                    ProductoId = producto.Id,
                    Producto = producto,
                    SessionId = sessionId,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio
                };

                _context.Carritos.Add(carritoItem);
            }

            _context.SaveChanges();

            // Si quieres actualizar el carrito sin recargar la página, puedes hacer algo como esto:
            int count = _context.Carritos.Where(c => c.SessionId == sessionId).Sum(c => c.Cantidad);
            return Json(new { success = true, carritoCount = count });
        }


        // Acción para agregar un producto al carrito
        //[HttpPost]
        //public IActionResult Agregar(int productoId, int cantidad)
        //{
        //    string sessionId = GetSessionId();
        //    var producto = _context.Productos.Find(productoId);

        //    if (producto == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuarioId = User.Identity.IsAuthenticated ? User.Identity.Name : null;

        //    var carritoItem = _context.Carritos
        //        .FirstOrDefault(c => c.ProductoId == productoId && c.SessionId == sessionId);

        //    if (carritoItem != null)
        //    {
        //        // Si el producto ya está en el carrito, actualiza la cantidad
        //        carritoItem.Cantidad += cantidad;
        //    }
        //    else
        //    {
        //        // Si el producto no está en el carrito, agrégalo
        //        carritoItem = new Carrito
        //        {
        //            ProductoId = producto.Id,
        //            Producto = producto,
        //            SessionId = sessionId,
        //            Cantidad = cantidad,
        //            PrecioUnitario = producto.Precio
        //        };

        //        _context.Carritos.Add(carritoItem);
        //    }

        //    _context.SaveChanges();
        //    return RedirectToAction("Index", "Carrito");
        //}

        // Acción para eliminar un producto del carrito
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var carritoItem = _context.Carritos.Find(id);

            if (carritoItem == null)
            {
                return NotFound();
            }

            _context.Carritos.Remove(carritoItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Método auxiliar para obtener o generar el SessionId
        private string GetSessionId()
        {
            if (HttpContext.Session.GetString("SessionId") == null)
            {
                HttpContext.Session.SetString("SessionId", System.Guid.NewGuid().ToString());
            }

            return HttpContext.Session.GetString("SessionId");
        }

        public IActionResult GetCarritoCount()
        {
            int count = _context.Carritos
                .Where(c => c.UsuarioId == GetSessionId())
                .Sum(c => c.Cantidad);

            return Content(count.ToString());
        }

    }
}
