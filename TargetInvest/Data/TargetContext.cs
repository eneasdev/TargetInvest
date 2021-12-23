using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TargetInvest.Entities;

namespace TargetInvest.Data
{
    public class TargetContext : DbContext
    {
        public TargetContext(DbContextOptions<TargetContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.Id)
                .HasName("PK_Clientes");

            modelBuilder.Entity<Endereco>()
                .HasKey(e => e.ClienteId)
                .HasName("PK_Enderecos");
        }
    }
}
