using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Commands;
using TodoApp.Application.DTOs;
using TodoApp.Application.Queries;

namespace TodoApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> Create(CreateTodoDto createTodoDto)
    {
        var command = new CreateTodoCommand(createTodoDto);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll()
    {
        var query = new GetAllTodosQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // Implement other actions similarly using MediatR commands/queries
}