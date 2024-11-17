using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Common.Mappings;

public static class TodoMappings
{
    public static TodoDto ToDto(this Todo todo) =>
        new(
            todo.Id,
            todo.Title,
            todo.Description,
            todo.IsComplete,
            todo.DueDate,
            todo.Priority.ToString(),
            todo.CreatedAt,
            todo.UpdatedAt
        );
}