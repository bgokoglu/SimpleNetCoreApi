using LazyCache;
using MediatR;
using SimpleAPI.Commands;
using SimpleAPI.Core;
using SimpleAPI.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.Handlers
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Response>
    {
        private readonly IAppCache _cache;

        public CreateBookHandler(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<Response> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            List<Book> books = await _cache.GetAsync<List<Book>>("books_in_cache").ConfigureAwait(false) ?? new List<Book>();

            var book = new Book { Id = books.Count + 1, Title = request.Title };
            books.Add(book);
            _cache.Add("books_in_cache", books);

            return new Response(book);
        }
    }
}
