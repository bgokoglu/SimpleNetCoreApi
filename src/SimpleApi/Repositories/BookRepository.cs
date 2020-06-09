using SimpleAPI.Domain;
using SimpleAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly List<Book> books = new List<Book>();

        public BookRepository()
        {
            for (int i = 1; i < 6; i++)
            {
                books.Add(new Book { Id = i, Title = "Book " + i });
            }
        }

        public async Task<Book> Add(Book book)
        {
            book.Id = books.Count + 1;
            books.Add(book);
            return await Task.FromResult(book).ConfigureAwait(false);
        }

        public async Task<bool> Delete(int id)
        {
            var item = books.Find(p => p.Id == id);

            if (item != null)
                books.Remove(item);

            return await Task.FromResult(true).ConfigureAwait(false);
        }

        public async Task<List<Book>> GetAll() => await Task.FromResult(books).ConfigureAwait(false);

        public async Task<Book> GetById(int id) => await Task.FromResult(books.Find(p => p.Id == id)).ConfigureAwait(false);

        public async Task<Book> Update(Book book)
        {
            var item = books.Find(p => p.Id == book.Id);

            if (item != null)
            {
                item.Title = book.Title;
            }
            return await Task.FromResult(item).ConfigureAwait(false);
        }
    }
}
