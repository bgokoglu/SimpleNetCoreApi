using LazyCache;
using MediatR;
using SimpleAPI.Commands;
using SimpleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IAppCache _cache;

        public DeleteBookHandler(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var books = await _cache.GetAsync<List<Book>>("books_in_cache").ConfigureAwait(false) ?? new List<Book>();

            var book = books.Find(p => p.Id == request.Id);

            if (book != null)
            {
                books.Remove(book);
                _cache.Add("books_in_cache", books);
            }

            return book;
        }
    }
}
