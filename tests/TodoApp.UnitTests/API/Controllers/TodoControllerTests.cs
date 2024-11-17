using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp.API.Controllers;
using TodoApp.Application.Commands;
using TodoApp.Application.Queries;
using TodoApp.Domain.Entities;
using Xunit;

namespace TodoApp.UnitTests.API.Controllers
{
    public class TodoControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TodoController _controller;

        public TodoControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TodoController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithTodos()
        {
            // Arrange
            var todos = new List<TodoDto>
            {
                new TodoDto { Id = 1, Title = "Test Todo 1", Description = "Description 1" },
                new TodoDto { Id = 2, Title = "Test Todo 2", Description = "Description 2" }
            };

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetAllTodosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(todos);

            // Act
            var result = await _controller.GetAll(CancellationToken.None);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedTodos = okResult.Value.Should().BeAssignableTo<IEnumerable<TodoDto>>().Subject;
            returnedTodos.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetById_ExistingTodo_ShouldReturnOk()
        {
            // Arrange
            var todoDto = new TodoDto { Id = 1, Title = "Test Todo", Description = "Description" };

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(todoDto);

            // Act
            var result = await _controller.GetById(1, CancellationToken.None);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedTodo = okResult.Value.Should().BeOfType<TodoDto>().Subject;
            returnedTodo.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetById_NonExistingTodo_ShouldReturnNotFound()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<GetTodoByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((TodoDto)null);

            // Act
            var result = await _controller.GetById(999, CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ValidTodo_ShouldReturnCreated()
        {
            // Arrange
            var command = new CreateTodoCommand
            {
                Title = "New Todo",
                Description = "New Description",
                DueDate = DateTime.Now.AddDays(1)
            };

            var createdTodo = new TodoDto { Id = 1, Title = command.Title, Description = command.Description };

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<CreateTodoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdTodo);

            // Act
            var result = await _controller.Create(command, CancellationToken.None);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.ActionName.Should().Be(nameof(TodoController.GetById));
            createdResult.RouteValues["id"].Should().Be(1);
        }

        [Fact]
        public async Task Update_ExistingTodo_ShouldReturnNoContent()
        {
            // Arrange
            var command = new UpdateTodoCommand
            {
                Id = 1,
                Title = "Updated Todo",
                Description = "Updated Description"
            };

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<UpdateTodoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, command, CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Update_NonExistingTodo_ShouldReturnNotFound()
        {
            // Arrange
            var command = new UpdateTodoCommand
            {
                Id = 999,
                Title = "Updated Todo",
                Description = "Updated Description"
            };

            _mediatorMock
                .Setup(x => x.Send(It.IsAny<UpdateTodoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Update(999, command, CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ExistingTodo_ShouldReturnNoContent()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<DeleteTodoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(1, CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Delete_NonExistingTodo_ShouldReturnNotFound()
        {
            // Arrange
            _mediatorMock
                .Setup(x => x.Send(It.IsAny<DeleteTodoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(999, CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}