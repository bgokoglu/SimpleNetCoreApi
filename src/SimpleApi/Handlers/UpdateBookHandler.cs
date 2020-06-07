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
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Response>
    {
        private readonly IAppCache _cache;

        public UpdateBookHandler(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<Response> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var books = await _cache.GetAsync<List<Book>>("books_in_cache").ConfigureAwait(false) ?? new List<Book>();

            var book = books.Find(p => p.Id == request.Id);

            if (book != null)
            {
                book.Title = request.Title;
                _cache.Add("books_in_cache", books);
            }

            return new Response(book);
        }
    }
}
