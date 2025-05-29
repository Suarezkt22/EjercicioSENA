using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaInterrapidisimo.Domain.Entities;

namespace PruebaTecnicaInterrapidisimo.Infraestructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
               .HasColumnName("Id")
               .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Credits)
               .IsRequired();

        builder.HasOne(c => c.Teacher)
               .WithMany(t => t.Courses)
               .HasForeignKey("TeacherId")
               .OnDelete(DeleteBehavior.Restrict); 

        builder.Metadata.FindNavigation(nameof(Course.Programs))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}