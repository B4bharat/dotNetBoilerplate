using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TodoApp.Application.Commands;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;
using Xunit;

namespace TodoApp.UnitTests.Application.Commands
{
    public class CreateTodoCommandHandlerTests
    {
        private readonly Mock<ITodoRepository> _todoRepositoryMock;
        private readonly CreateTodoCommandHandler _handler;

        public CreateTodoCommandHandlerTests()
        {
            _todoRepositoryMock = new Mock<ITodoRepository>();
            _handler = new CreateTodoCommandHandler(_todoRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateTodo()
        {
            // Arrange
            var command = new CreateTodoCommand
            {
                Title = "Test Todo",
                Description = "Test Description",
                DueDate = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            _todoRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Todo>()), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyTitle_ShouldThrowValidationException()
        {
            // Arrange
            var command = new CreateTodoCommand
            {
                Title = "",
                Description = "Test Description"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}