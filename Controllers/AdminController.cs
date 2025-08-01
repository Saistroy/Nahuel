using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DeliveryLocal.Data;
using DeliveryLocal.Models;
using System.Linq;

namespace DeliveryLocal.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("AdminLogged", "true");
                return RedirectToAction("Pedidos");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        public IActionResult Pedidos(string filtroEstado)
        {
            if (HttpContext.Session.GetString("AdminLogged") != "true")
                return RedirectToAction("Login");

            var pedidos = string.IsNullOrEmpty(filtroEstado)
                ? _context.Pedidos.ToList()
                : _context.Pedidos.Where(p => p.Estado == filtroEstado).ToList();

            ViewBag.FiltroEstado = filtroEstado;
            return View(pedidos);
        }

        [HttpPost]
        public IActionResult ActualizarEstado(int id, string estado)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido != null)
            {
                if (estado == "Entregado")
                {
                    _context.Pedidos.Remove(pedido); // Eliminar si está entregado
                }
                else
                {
                    pedido.Estado = estado;
                    _context.Pedidos.Update(pedido); // Actualizar si no está entregado
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Pedidos");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminLogged");
            return RedirectToAction("Login");
        }
    }
}
