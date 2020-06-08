using LazyCache;
using SimpleAPI.Domain;
using SimpleAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPI.Repositories.Cached
{
    public class CachedBookRepository : IRepository<Book>
    {
        private readonly IRepository<Book> _repository;
        private readonly IAppCache _cache;

        public CachedBookRepository(IRepository<Book> repository, IAppCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<Book> Add(Book book)
        {
            var newBook = await _repository.Add(book).ConfigureAwait(false);

            if (newBook.Id != 0)
                ClearCache();

            return await Task.FromResult(newBook).ConfigureAwait(false);
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _repository.Delete(id).ConfigureAwait(false);

            if (result)
                ClearCache();

            return await Task.FromResult(result).ConfigureAwait(false);
        }

        public async Task<List<Book>> GetAll()
        {
            var books = _cache.Get<List<Book>>("books_in_cache");

            if (books == null)
            {
                books = await _repository.GetAll().ConfigureAwait(false);
                _cache.Add("books_in_cache", books);
            }

            return await Task.FromResult(books).ConfigureAwait(false);
        }

        public async Task<Book> GetById(int id)
        {
            var book = _cache.Get<Book>($"book_{id}_in_cache");

            if (book == null)
            {
                book = await _repository.GetById(id).ConfigureAwait(false);
                _cache.Add($"book_{id}_in_cache", book);
            }

            return await Task.FromResult(book).ConfigureAwait(false);
        }

        public async Task<Book> Update(Book book)
        {
            var updatedBook = await _repository.Update(book).ConfigureAwait(false);
            _cache.Add($"book_{book.Id}_in_cache", updatedBook);
            return await Task.FromResult(updatedBook).ConfigureAwait(false);
        }

        private void ClearCache() => _cache.Remove("books_in_cache");
    }
}
