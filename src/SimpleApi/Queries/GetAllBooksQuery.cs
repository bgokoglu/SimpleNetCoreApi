using MediatR;
using SimpleAPI.Core;
using SimpleAPI.Domain;
using System.Collections.Generic;

namespace SimpleAPI.Queries
{
    public class GetAllBooksQuery : IRequest<Response>
    {
    }
}
