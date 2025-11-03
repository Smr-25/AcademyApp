using AcademyApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyApp.DLL.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(s => s.LastName).HasMaxLength(50).IsRequired();
        builder.Property(s => s.Email).HasMaxLength(100).IsRequired();
        builder.HasIndex(s => s.Email).IsUnique();
        builder.Property(s => s.Email).HasAnnotation("RegularExpression", @"^[a-zA-Z0-9._%+-]+@code\.edu\.az$");
        builder.Property(s => s.DateOfBirth).IsRequired();

        builder.HasOne(s=>s.Group)
            .WithMany(g=>g.Students)
            .HasForeignKey(s=>s.GroupId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
            
        builder.HasData(
            new Student
            {
                Id = 1,
                FirstName = "Aysel",
                LastName = "Mammadova",
                Email = "aysel.mammadova@code.edu.az",
                DateOfBirth = new DateTime(2000, 5, 15),
                GroupId = 1
            },
            new Student
            {
                Id = 2,
                FirstName = "Elvin",
                LastName = "Aliyev",
                Email = "elvin.aliyev@code.edu.az",
                DateOfBirth = new DateTime(2001, 3, 22),
                GroupId = 1
            },
            new Student
            {
                Id = 3,
                FirstName = "Leyla",
                LastName = "Hasanova",
                Email = "leyla.hasanova@code.edu.az",
                DateOfBirth = new DateTime(1999, 8, 10),
                GroupId = 2
            },
            new Student
            {
                Id = 4,
                FirstName = "Rəşad",
                LastName = "Quliyev",
                Email = "reshad.quliyev@code.edu.az",
                DateOfBirth = new DateTime(2002, 1, 5),
                GroupId = 2
            },
            new Student
            {
                Id = 5,
                FirstName = "Nərmin",
                LastName = "Əhmədova",
                Email = "nermin.ahmadova@code.edu.az",
                DateOfBirth = new DateTime(2000, 11, 30),
                GroupId = 3
            },
            new Student
            {
                Id = 6,
                FirstName = "Tural",
                LastName = "İbrahimov",
                Email = "tural.ibrahimov@code.edu.az",
                DateOfBirth = new DateTime(2001, 7, 18),
                GroupId = 1
            },
            new Student
            {
                Id = 7,
                FirstName = "Günel",
                LastName = "Məmmədli",
                Email = "gunel.mammadli@code.edu.az",
                DateOfBirth = new DateTime(1998, 12, 25),
                GroupId = 1
            }
        );
    }
}