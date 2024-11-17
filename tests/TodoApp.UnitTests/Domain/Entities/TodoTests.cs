using System;
using FluentAssertions;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Exceptions;
using Xunit;

namespace TodoApp.UnitTests.Domain.Entities
{
    public class TodoTests
    {
        [Fact]
        public void Create_ValidParameters_ShouldCreateTodo()
        {
            // Arrange & Act
            var todo = Todo.Create(
                "Test Title",
                "Test Description",
                DateTime.Now.AddDays(1)
            );

            // Assert
            todo.Should().NotBeNull();
            todo.Title.Should().Be("Test Title");
            todo.Description.Should().Be("Test Description");
            todo.IsCompleted.Should().BeFalse();
        }

        [Fact]
        public void MarkAsComplete_IncompleteTodo_ShouldMarkAsComplete()
        {
            // Arrange
            var todo = Todo.Create(
                "Test Title",
                "Test Description",
                DateTime.Now.AddDays(1)
            );

            // Act
            todo.MarkAsComplete();

            // Assert
            todo.IsCompleted.Should().BeTrue();
            todo.CompletedDate.Should().NotBeNull();
        }

        [Fact]
        public void Create_PastDueDate_ShouldThrowDomainException()
        {
            // Arrange & Act
            Action action = () => Todo.Create(
                "Test Title",
                "Test Description",
                DateTime.Now.AddDays(-1)
            );

            // Assert
            action.Should().Throw<DomainException>()
                .WithMessage("Due date cannot be in the past");
        }
    }
}