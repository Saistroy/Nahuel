using DeliveryLocal.Models;
using System;
using System.Linq;

namespace DeliveryLocal.Data
{
    public static class DbInitializer
    {
        public static void Inicializar(AppDbContext context)
        {
            // Crea la base si no existe
            context.Database.EnsureCreated();

            // Corregir fechas vacías o nulas en pedidos
            var pedidosInvalidos = context.Pedidos
                .Where(p => p.Fecha == default)
                .ToList();

            foreach (var pedido in pedidosInvalidos)
            {
                pedido.Fecha = DateTime.Now;
            }

            if (pedidosInvalidos.Count > 0)
            {
                context.SaveChanges();
            }

            // Si ya existe un usuario, no hace nada (pero sin return para seguir con la corrección)
            if (!context.Usuarios.Any())
            {
                // Agrega el usuario admin
                var admin = new Usuario
                {
                    Username = "admin",
                    Password = "saistroy"
                };

                context.Usuarios.Add(admin);
                context.SaveChanges();
            }
        }
    }
}

