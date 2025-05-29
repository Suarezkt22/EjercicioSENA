using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaInterrapidisimo.Domain.Entities;
using DomainProgram = PruebaTecnicaInterrapidisimo.Domain.Entities.Program;

namespace PruebaTecnicaInterrapidisimo.Infraestructure.Persistence.Configurations;

public class ProgramConfiguration : IEntityTypeConfiguration<DomainProgram>
{
    public void Configure(EntityTypeBuilder<DomainProgram> builder)
    {
        builder.ToTable("Programs");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
               .HasColumnName("Id")
               .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Credits)
               .IsRequired();

        builder.HasMany(p => p.Courses)
               .WithMany(c => c.Programs)
               .UsingEntity<Dictionary<string, object>>(
                   "ProgramCourses",
                   j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                   j => j.HasOne<DomainProgram>().WithMany().HasForeignKey("ProgramId"),
                   j => j.ToTable("ProgramCourses")
               );
    }
}