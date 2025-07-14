using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain;
using TaskManagement.Persistence.Utils;

namespace TaskManagement.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(nameof(TaskManagementDbContext.Comments), schema: PersistenceConstants.TaskManagement_SCHEMA);

        builder.Property(c => c.Content).IsRequired();
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.TaskId).IsRequired();

        // User relationship
        builder.HasOne(c => c.User)
               .WithMany() // No navigation on User side
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Task relationship
        builder.HasOne(c => c.Task)
               .WithMany(t => t.Comments) // Navigation on Task side
               .HasForeignKey(c => c.TaskId)
               .OnDelete(DeleteBehavior.Cascade); // Delete comments when task is deleted
    }
}


/*
 * 
 *Comments Table:
Id          | Content                    | UserId   | TaskId    | CreatedAt
------------|----------------------------|----------|-----------|------------
comment-1   | "This looks great!"        | john-id  | task-1    | 2024-01-15
comment-2   | "Need to fix the UI"       | sarah-id | task-1    | 2024-01-16
comment-3   | "Ready for review"         | john-id  | task-2    | 2024-01-17
comment-4   | "Testing completed"        | sarah-id | task-3    | 2024-01-18 
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
 * 
 * 
 * **/