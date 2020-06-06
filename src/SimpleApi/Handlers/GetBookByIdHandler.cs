using LazyCache;
using MediatR;
using SimpleAPI.Models;
using SimpleAPI.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IAppCache _cache;

        public GetBookByIdHandler(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var books = await _cache.GetAsync<List<Book>>("books_in_cache").ConfigureAwait(false);

            if (books == null || books.Count == 0)
                return null;

            return books.Find(p => p.Id == request.BookId);
        }
    }
}
