using TaskManagement.Domain.Projects;
using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Domain.Tasks;

public class Task
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required User CreatedBy { get; set; }
    //public Guid CreatedById { get; set; }
    public Guid? AssignedToId { get; set; }
    public List<User> AssignedTo { get; set; } = new List<User>();
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Label> Labels { get; set; } = new List<Label>();
    public Guid ProjectId { get; set; }
    public required Project Project { get; set; }
    public TaskStatus TaskStatus { get; set; } = TaskStatus.Todo;
    public PriorityStatus PriorityStatus { get; set; } = PriorityStatus.Low;
}
