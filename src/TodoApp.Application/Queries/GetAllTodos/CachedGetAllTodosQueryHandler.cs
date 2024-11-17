using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApp.Infrastructure.Caching;

namespace TodoApp.Application.Queries.GetAllTodos
{
    public class CachedGetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, IEnumerable<TodoDto>>
    {
        private readonly IRequestHandler<GetAllTodosQuery, IEnumerable<TodoDto>> _decorated;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "all_todos";

        public CachedGetAllTodosQueryHandler(
            IRequestHandler<GetAllTodosQuery, IEnumerable<TodoDto>> decorated,
            ICacheService cacheService)
        {
            _decorated = decorated;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<TodoDto>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            // Try to get from cache first
            var cached = await _cacheService.GetAsync<IEnumerable<TodoDto>>(CacheKey);
            if (cached != null)
            {
                return cached;
            }

            // If not in cache, get from original handler
            var result = await _decorated.Handle(request, cancellationToken);

            // Store in cache
            await _cacheService.SetAsync(CacheKey, result);

            return result;
        }
    }
}