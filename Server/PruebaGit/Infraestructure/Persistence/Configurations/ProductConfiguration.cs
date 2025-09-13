using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GitEjercicioSENA.Domain.Entities;
using GitEjercicioSENA.Domain.Entities.ValueObjects;

namespace GitEjercicioSENA.Infraestructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
               .HasColumnName("Id")
               .ValueGeneratedOnAdd();

        builder.OwnsOne(c => c.Nombre, nombre =>
        {
            nombre.Property(n => n.Value)
                  .HasColumnName("Nombre")
                  .IsRequired()
                  .HasMaxLength(100);
        });

        builder.OwnsOne(c => c.Descripcion, descripcion =>
        {
            descripcion.Property(d => d.Value)
                       .HasColumnName("Descripcion")
                       .IsRequired()
                       .HasMaxLength(500);
        });

        builder.OwnsOne(c => c.Stock, stock =>
        {
            stock.Property(s => s.Value)
                 .HasColumnName("Stock")
                 .IsRequired();
        });

        builder.OwnsOne(c => c.Precio, precio =>
        {
            precio.Property(p => p.Value)
                  .HasColumnName("Precio")
                  .IsRequired()
                  .HasPrecision(18, 2);
        });

        builder.Property(c => c.FechaCreacion)
               .IsRequired();
    }
}
