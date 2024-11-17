namespace TodoApp.Domain.Entities;

public class Todo
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsComplete { get; private set; }
    public DateTime? DueDate { get; private set; }
    public Priority Priority { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Todo(string title, string? description, DateTime? dueDate, Priority priority)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string title, string? description, DateTime? dueDate, Priority priority)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkComplete()
    {
        IsComplete = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkIncomplete()
    {
        IsComplete = false;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum Priority
{
    Low,
    Medium,
    High
}