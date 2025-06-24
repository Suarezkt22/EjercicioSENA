using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaRaddarStudios.Domain.Entities;

namespace PruebaRaddarStudios.Infraestructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
               .HasColumnName("Id")
               .ValueGeneratedOnAdd();

        builder.Property(c => c.Email);

        builder.Property(c => c.Password);
    }
}
