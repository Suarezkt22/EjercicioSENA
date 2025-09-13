using System.Reflection;
using Microsoft.EntityFrameworkCore;
using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Infraestructure.Persistence;

public class DbWriteContext(DbContextOptions<DbWriteContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

