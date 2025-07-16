using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain;
using TaskManagement.Domain.UserManagement;
using TaskManagement.Persistence.Utils;

namespace TaskManagement.Persistence.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Tasks.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Tasks.Task> builder)
        {
            builder.ToTable(nameof(TaskManagementDbContext.Tasks), schema: PersistenceConstants.TaskManagement_SCHEMA);

            // Primary key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200); // Adjust max length as needed

            builder.Property(t => t.Description)
                   .HasMaxLength(1000); // Optional, so no IsRequired()

            builder.Property(t => t.StartDate)
                   .IsRequired(false);

            builder.Property(t => t.EndDate)
                   .IsRequired(false);

            builder.Property(t => t.TaskStatus)
                   .IsRequired()
                   .HasConversion<string>(); // Store enum as string

            builder.Property(t => t.PriorityStatus)
                   .IsRequired()
                   .HasConversion<string>(); // Store enum as string

            builder.Property(t => t.ProjectId)
                   .IsRequired();

            // Relationships

            // CreatedBy relationship (required)
            builder.HasOne(t => t.CreatedBy)
                   .WithMany() // No navigation on User side
                   .HasForeignKey("CreatedById") // Shadow property
                   .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of user if they have created tasks

            // AssignedTo relationship (many-to-many)
            //builder.HasMany(t => t.Assignees)
            //       .WithMany(t => t.Tasks)
            //       .UsingEntity(m =>
            //       {
            //           m.ToTable("TaskAssignee");
            //           m.Property("TaskId");
            //           m.Property("UserId");
            //       });

            //..is coming from your many-to-many configuration in the .UsingEntity(...) method. You're creating a join table (TaskAssignee) but not specifying the property types of TaskId and UserId. EF Core doesn't know what type they are (shadow properties), so you must explicitly provide the type.

            builder.HasMany(t => t.Assignees)
       .WithMany(t => t.Tasks)
       .UsingEntity<Dictionary<string, object>>(
           "TaskAssignee",
           j => j.HasOne<User>()
                 .WithMany()
                 .HasForeignKey("UserId")
                 .OnDelete(DeleteBehavior.Cascade),
           j => j.HasOne<Domain.Tasks.Task>()
                 .WithMany()
                 .HasForeignKey("TaskId")
                 .OnDelete(DeleteBehavior.Cascade),
           j =>
           {
               j.HasKey("TaskId", "UserId");
               j.ToTable("TaskAssignee");
               j.Property<Guid>("TaskId");
               j.Property<Guid>("UserId");
           });

            // Project relationship (required)
            builder.HasOne(t => t.Project)
                   .WithMany(p => p.Tasks) // Navigation on Project side
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade); // Delete tasks when project is deleted

            // Comments relationship (one-to-many)
            builder.HasMany(t => t.Comments)
                   .WithOne(c => c.Task)
                   .HasForeignKey(c => c.TaskId)
                   .OnDelete(DeleteBehavior.Cascade); // Delete comments when task is deleted

            //builder.HasMany(t => t.Labels)
            //       .WithMany(t => t.Tasks)
            //       .UsingEntity(m =>
            //       {
            //           m.ToTable("TaskLabels");
            //           m.Property("TaskId");
            //           m.Property("LabelId");
            //       });

            builder.HasMany(t => t.Labels)
       .WithMany(t => t.Tasks)
       .UsingEntity<Dictionary<string, object>>(
           "TaskLabels",
           j => j.HasOne<Label>()
                 .WithMany()
                 .HasForeignKey("LabelId")
                 .OnDelete(DeleteBehavior.Cascade),
           j => j.HasOne<Domain.Tasks.Task>()
                 .WithMany()
                 .HasForeignKey("TaskId")
                 .OnDelete(DeleteBehavior.Cascade),
           j =>
           {
               j.HasKey("TaskId", "LabelId");
               j.ToTable("TaskLabels");
               j.Property<Guid>("TaskId");
               j.Property<Guid>("LabelId");
           });
        }
    }
}

/*
 * Id      | Title              | Description        | StartDate  | EndDate    | TaskStatus | PriorityStatus | ProjectId | CreatedById | AssignedToId
--------|--------------------|--------------------|------------|------------|------------|----------------|-----------|-------------|-------------
task-1  | Design Homepage    | Create new layout  | 2024-01-10 | 2024-01-20 | InProgress | High          | proj-1    | john-id     | sarah-id
task-2  | Setup Database     | Configure MySQL    | 2024-01-15 | NULL       | Completed  | Medium        | proj-1    | sarah-id    | john-id
task-3  | User Authentication| Implement OAuth    | NULL       | 2024-01-25 | Pending    | High          | proj-2    | john-id     | NULL
task-4  | Write Tests        | Unit & Integration | 2024-01-18 | 2024-01-22 | InProgress | Low           | proj-2    | sarah-id    | sarah-id
 * 
 * 
 * 
 * 
 * 
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