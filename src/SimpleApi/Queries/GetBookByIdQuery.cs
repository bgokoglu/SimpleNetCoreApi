using MediatR;
using SimpleAPI.Core;
using SimpleAPI.Domain;

namespace SimpleAPI.Queries
{
    public class GetBookByIdQuery : IRequest<Response>
    {
        public int BookId { get; }

        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }
    }
}
