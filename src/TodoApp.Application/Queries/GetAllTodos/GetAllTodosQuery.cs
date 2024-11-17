using MediatR;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Queries;

public record GetAllTodosQuery() : IRequest<IEnumerable<TodoDto>>;