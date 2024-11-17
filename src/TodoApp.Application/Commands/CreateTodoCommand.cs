using MediatR;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Commands;

public record CreateTodoCommand(CreateTodoDto Todo) : IRequest<TodoDto>;