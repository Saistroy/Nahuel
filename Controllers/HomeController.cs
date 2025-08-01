using Microsoft.AspNetCore.Mvc;
using DeliveryLocal.Data;
using DeliveryLocal.Models;
using System.Linq;

namespace DeliveryLocal.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View(new Pedido());
        }

        [HttpPost("/")]
        public IActionResult CrearPedido(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.Fecha = DateTime.Now;
                pedido.Estado = "Pendiente";
                _context.Pedidos.Add(pedido);
                _context.SaveChanges();

                // Redirige a la confirmación con el ID del pedido
                return RedirectToAction("Confirmacion", new { id = pedido.Id });
            }

            return View("Index", pedido);
        }

        [HttpGet("confirmacion/{id}")]
        public IActionResult Confirmacion(int id)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido == null)
            {
                return NotFound("El pedido no fue encontrado.");
            }

            return View("Confirmacion", pedido);
        }
    }
}
