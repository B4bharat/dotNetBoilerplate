using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using TodoApp.Application.Queries.GetAllTodos;
using TodoApp.Infrastructure.Caching;
using Xunit;

namespace TodoApp.UnitTests.Application.Queries.GetAllTodos
{
    public class CachedGetAllTodosQueryHandlerTests
    {
        private readonly Mock<IRequestHandler<GetAllTodosQuery, IEnumerable<TodoDto>>> _decoratedHandler;
        private readonly Mock<ICacheService> _cacheService;
        private readonly CachedGetAllTodosQueryHandler _handler;

        public CachedGetAllTodosQueryHandlerTests()
        {
            _decoratedHandler = new Mock<IRequestHandler<GetAllTodosQuery, IEnumerable<TodoDto>>>();
            _cacheService = new Mock<ICacheService>();
            _handler = new CachedGetAllTodosQueryHandler(_decoratedHandler.Object, _cacheService.Object);
        }

        [Fact]
        public async Task Handle_CacheHit_ShouldReturnCachedResult()
        {
            // Arrange
            var cachedTodos = new List<TodoDto>
            {
                new TodoDto { Id = 1, Title = "Cached Todo" }
            };

            _cacheService
                .Setup(x => x.GetAsync<IEnumerable<TodoDto>>("all_todos"))
                .ReturnsAsync(cachedTodos);

            // Act
            var result = await _handler.Handle(new GetAllTodosQuery(), CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(cachedTodos);
            _decoratedHandler.Verify(x => x.Handle(It.IsAny<GetAllTodosQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_CacheMiss_ShouldCallDecoratedHandlerAndCache()
        {
            // Arrange
            var todos = new List<TodoDto>
            {
                new TodoDto { Id = 1, Title = "Test Todo" }
            };

            _cacheService
                .Setup(x => x.GetAsync<IEnumerable<TodoDto>>("all_todos"))
                .ReturnsAsync((IEnumerable<TodoDto>)null);

            _decoratedHandler
                .Setup(x => x.Handle(It.IsAny<GetAllTodosQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(todos);

            // Act
            var result = await _handler.Handle(new GetAllTodosQuery(), CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(todos);
            _decoratedHandler.Verify(x => x.Handle(It.IsAny<GetAllTodosQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _cacheService.Verify(x => x.SetAsync("all_todos", todos, null), Times.Once);
        }
    }
}