using Pedidos_ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace Pedidos_ASP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
    }
}