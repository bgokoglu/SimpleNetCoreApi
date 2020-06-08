using LazyCache;
using MediatR;
using SimpleAPI.Core;
using SimpleAPI.Domain;
using SimpleAPI.Queries;
using SimpleAPI.Repositories.Interface;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, Response>
    {
        private readonly IRepository<Book> _repository;

        public GetAllBooksHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repository.GetAll().ConfigureAwait(false);
            return new Response(books);
        }
    }
}
