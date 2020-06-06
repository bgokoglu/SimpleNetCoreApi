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
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly IAppCache _cache;

        public GetAllBooksHandler(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _cache.GetAsync<List<Book>>("books_in_cache").ConfigureAwait(false) ?? new List<Book>();
        }
    }
}
