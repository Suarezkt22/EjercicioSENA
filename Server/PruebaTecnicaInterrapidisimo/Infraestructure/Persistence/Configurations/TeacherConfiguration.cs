using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaInterrapidisimo.Domain.Entities;

namespace PruebaTecnicaInterrapidisimo.Infraestructure.Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");

        // Primary Key
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
               .HasColumnName("Id")
               .ValueGeneratedOnAdd();

        // Properties
        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(100);
    }
}