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
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Response>
    {
        private readonly IAppCache _cache;

        public DeleteBookHandler(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<Response> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var books = await _cache.GetAsync<List<Book>>("books_in_cache").ConfigureAwait(false) ?? new List<Book>();

            var book = books.Find(p => p.Id == request.Id);

            if (book != null)
            {
                books.Remove(book);
                _cache.Add("books_in_cache", books);
            }

            return new Response(book);
        }
    }
}
