using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TargetInvest.Domain.Entities;

namespace TargetInvest.Infrastructure
{
    public class TargetContext : DbContext
    {
        public TargetContext(DbContextOptions<TargetContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Vip> Vips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Endereco);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Vip);
        }
    }
}
