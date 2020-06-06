using MediatR;
using SimpleAPI.Models;
using System.Collections.Generic;

namespace SimpleAPI.Queries
{
    public class GetAllBooksQuery : IRequest<List<Book>>
    {
    }
}
