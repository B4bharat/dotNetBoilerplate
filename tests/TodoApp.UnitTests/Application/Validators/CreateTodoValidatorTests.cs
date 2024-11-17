using System;
using FluentValidation.TestHelper;
using TodoApp.Application.Commands;
using TodoApp.Application.Validators;
using Xunit;

namespace TodoApp.UnitTests.Application.Validators
{
    public class CreateTodoValidatorTests
    {
        private readonly CreateTodoValidator _validator;

        public CreateTodoValidatorTests()
        {
            _validator = new CreateTodoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            // Arrange
            var command = new CreateTodoCommand
            {
                Title = "",
                Description = "Test Description"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateTodoCommand
            {
                Title = "Valid Title",
                Description = "Valid Description",
                DueDate = DateTime.Now.AddDays(1)
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}