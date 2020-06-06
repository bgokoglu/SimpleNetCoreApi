using MediatR;
using SimpleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
