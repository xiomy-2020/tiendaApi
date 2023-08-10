using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TiendaProductosApi.Models;

namespace TiendaProductosApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<VentaProducto> Ventas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Ventas)
                .WithMany(v => v.Productos)
                .UsingEntity("ProductosVentas");

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.proveedor)
                .WithMany(pr => pr.Productos)
                .HasForeignKey(pr => pr.ProveedorId);

            modelBuilder.Entity<VentaProducto>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteId);

           
        }

    }
}
