using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GitEjercicioSENA.Domain.Entities;

namespace GitEjercicioSENA.Infraestructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
               .HasColumnName("Id")
               .ValueGeneratedOnAdd();

        builder.Property(c => c.Email)
               .IsRequired()
               .HasMaxLength(255);

        builder.HasIndex(c => c.Email)
               .IsUnique();

        builder.Property(c => c.Password)
               .IsRequired()
               .HasMaxLength(128);
    }
}
