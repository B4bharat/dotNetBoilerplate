using MediatR;
using TodoApp.Application.Common.Mappings;
using TodoApp.Application.DTOs;
using TodoApp.Domain.Repositories;

namespace TodoApp.Application.Queries;

public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, IEnumerable<TodoDto>>
{
    private readonly ITodoRepository _todoRepository;

    public GetAllTodosQueryHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<TodoDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        var todos = await _todoRepository.GetAllAsync();
        return todos.Select(todo => todo.ToDto());
    }
}