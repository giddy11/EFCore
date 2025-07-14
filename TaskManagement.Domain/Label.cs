namespace TaskManagement.Domain;

public class Label
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Color { get; set; }
}
