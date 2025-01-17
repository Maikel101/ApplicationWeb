using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace TuProyecto.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nombre,Apellido,Email,Telefono,Mensaje")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.FechaRegistro = DateTime.Now;
                _context.Add(cliente);
                _context.SaveChanges();
                TempData["Message"] = "El mensaje se ha enviado correctamente.";
                return RedirectToAction("Success");
            }
            return View(cliente);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}

