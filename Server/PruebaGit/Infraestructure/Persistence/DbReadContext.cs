using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PruebaRaddarStudios.Domain.Entities;

namespace PruebaRaddarStudios.Infraestructure.Persistence;

public class DbReadContext(DbContextOptions<DbReadContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}