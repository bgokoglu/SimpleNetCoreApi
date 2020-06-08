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
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Response>
    {
        private readonly IRepository<Book> _repository;

        public GetBookByIdHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetById(request.BookId).ConfigureAwait(false);
            return new Response(book);
        }
    }
}
