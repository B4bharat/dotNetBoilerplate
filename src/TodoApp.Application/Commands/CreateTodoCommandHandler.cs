using MediatR;
using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Application.Commands;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoDto>
{
    private readonly ITodoRepository _todoRepository;

    public CreateTodoCommandHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo(
            request.Todo.Title,
            request.Todo.Description,
            request.Todo.DueDate,
            Enum.Parse<Priority>(request.Todo.Priority)
        );

        var created = await _todoRepository.AddAsync(todo);

        return new TodoDto(
            created.Id,
            created.Title,
            created.Description,
            created.IsComplete,
            created.DueDate,
            created.Priority.ToString(),
            created.CreatedAt,
            created.UpdatedAt
        );
    }
}