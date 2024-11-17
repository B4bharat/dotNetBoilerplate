namespace TodoApp.Application.DTOs;

public record TodoDto(
    int Id,
    string Title,
    string? Description,
    bool IsComplete,
    DateTime? DueDate,
    string Priority,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record CreateTodoDto(
    string Title,
    string? Description,
    DateTime? DueDate,
    string Priority
);

public record UpdateTodoDto(
    string Title,
    string? Description,
    DateTime? DueDate,
    string Priority
);