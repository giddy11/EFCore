namespace TaskManagement.Domain;

public class Label
{
    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public IList<Domain.Tasks.Task> Tasks { get; set; } = new List<Domain.Tasks.Task>();
    public string? Color { get; set; }
}
