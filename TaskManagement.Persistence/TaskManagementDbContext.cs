using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using TaskManagement.Domain;
using TaskManagement.Domain.Projects;
using TaskManagement.Domain.UserManagement;
using System.Data;

namespace TaskManagement.Persistence;

public class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementDbContext).Assembly);
    }

    // Core Domain Entities
    public DbSet<Project> Projects { get; set; }
    public DbSet<Domain.Tasks.Task> Tasks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Label> Labels { get; set; }


    //user management entities
    public DbSet<User> Users { get; set; }
}
