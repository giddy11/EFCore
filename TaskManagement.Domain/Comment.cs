using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Domain;

public class Comment
{
    public Guid Id { get; init; }
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
    public string Content { get; set; } = default!;
    public User User { get; set; } = default!;
    public Tasks.Task Task { get; set; } = default!;
}
