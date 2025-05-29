using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaInterrapidisimo.Domain.Aggregates;
using PruebaTecnicaInterrapidisimo.Domain.Entities;

namespace PruebaTecnicaInterrapidisimo.Infraestructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(s => s.Id);
        builder.Property(e => e.Id).HasColumnName("Id");

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(s => s.Program)
            .WithMany(s => s.Students)
            .HasForeignKey("ProgramId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.EnrolledCourses)
            .WithMany(c => c.Students)
            .UsingEntity<Dictionary<string, object>>(
                "StudentCourses",
                j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                j => j.HasOne<Student>().WithMany().HasForeignKey("StudentId"))
            .ToTable("StudentCourses");

        builder.Metadata.FindNavigation(nameof(Student.EnrolledCourses))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
