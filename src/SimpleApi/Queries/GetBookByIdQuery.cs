using MediatR;
using SimpleAPI.Models;

namespace SimpleAPI.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int BookId { get; }

        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }
    }
}
