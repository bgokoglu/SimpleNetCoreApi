using LazyCache;
using MediatR;
using SimpleAPI.Core;
using SimpleAPI.Domain;
using SimpleAPI.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, Response>
    {
        private readonly IAppCache _cache;

        public GetAllBooksHandler(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<Response> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _cache.GetAsync<List<Book>>("books_in_cache").ConfigureAwait(false) ?? new List<Book>();

            return new Response(books);
        }
    }
}
