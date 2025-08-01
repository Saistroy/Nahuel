using Microsoft.EntityFrameworkCore;
using DeliveryLocal.Models;

namespace DeliveryLocal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
    }
}
