using AcademyApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyApp.DLL.Data.Configurations;

internal class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Groups");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Name).HasMaxLength(100).IsRequired(true);
        builder.Property(g => g.Description).HasMaxLength(100).IsRequired(true);
        builder.Property(g => g.Limit);
        builder.HasIndex(g=> g.Name).IsUnique();
        
        builder.HasMany(g=>g.Students)
            .WithOne(s=>s.Group)
            .HasForeignKey(s=>s.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new Group
        {
            Id = 1,
            Name = "Frontend Development",
            Description = "Learn to build stunning user interfaces with HTML, CSS, and JavaScript.",
            Limit = 20
        }, new Group
        {
            Id = 2,
            Name = "Backend Development",
            Description = "Master server-side programming and database management.",
            Limit = 15
        },
            new Group
        {
            Id = 3,
            Name = "Fullstack Development",
            Description = "Become proficient in both frontend and backend technologies.",
            Limit = 10
        });
    }
}