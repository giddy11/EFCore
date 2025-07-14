using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Domain.Projects;

public class Project
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Guid CreatedById { get; set; }
    public required User CreatedBy { get; set; }
    public DateTime StartDate { get; set; }
    public  DateTime EndDate { get; set; }
    public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Not_Started;
    public List<Domain.Tasks.Task>? Tasks { get; set; }
}
