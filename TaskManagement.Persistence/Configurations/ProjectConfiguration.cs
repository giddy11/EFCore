using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Projects;
using TaskManagement.Persistence.Utils;

namespace TaskManagement.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable(nameof(TaskManagementDbContext.Projects), schema: PersistenceConstants.TaskManagement_SCHEMA);

        // Properties
        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Description)
               .HasMaxLength(1000); // Optional, so no IsRequired()

        builder.Property(p => p.StartDate)
               .IsRequired();

        builder.Property(p => p.EndDate)
               .IsRequired();

        builder.Property(p => p.ProjectStatus)
               .IsRequired()
               .HasConversion<string>() // Store enum as string
               .HasDefaultValue(ProjectStatus.Not_Started);

        // Foreign key for CreatedBy
        builder.Property(p => p.CreatedById)
               .IsRequired();

        // User relationship
        builder.HasOne(p => p.CreatedBy)
               .WithMany() // No navigation on User side
               .HasForeignKey(p => p.CreatedById)
               .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if referenced

        // Tasks relationship
        builder.HasMany(p => p.Tasks)
               .WithOne(t => t.Project) // Assuming Task has Project navigation
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Cascade); // Delete tasks when project is deleted
    }
}

/*
 * 
 * Projects Table:
Id          | Title           | CreatedById
------------|-----------------|------------
guid-1      | Website Redesign| john-id
guid-2      | Mobile App      | john-id
guid-3      | Marketing       | sarah-id


Users Table:
Id      | FirstName | LastName
--------|-----------|----------
john-id | John      | Doe
sarah-id| Sarah     | Smith
 * 
 * 
 * 
 * 
 * Projects Table:
Id                  | Title
--------------------|------------------
website-project-id  | Website Redesign
mobile-project-id   | Mobile App

Tasks Table:
Id     | Title              | ProjectId
-------|--------------------|-----------------
task-1 | Design Homepage    | website-project-id
task-2 | Setup Database     | website-project-id
task-3 | Design UI Mockups  | mobile-project-id
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * **/