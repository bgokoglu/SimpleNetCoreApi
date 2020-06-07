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
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Response>
    {
        private readonly IAppCache _cache;

        public GetBookByIdHandler(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<Response> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var books = await _cache.GetAsync<List<Book>>("books_in_cache").ConfigureAwait(false);

            if (books == null || books.Count == 0)
                return new Response(null);

            return new Response(books.Find(p => p.Id == request.BookId));
        }
    }
}
