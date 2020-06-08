using LazyCache;
using MediatR;
using SimpleAPI.Commands;
using SimpleAPI.Core;
using SimpleAPI.Domain;
using SimpleAPI.Repositories.Interface;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Response>
    {
        private readonly IRepository<Book> _repository;

        public DeleteBookHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.Delete(request.Id).ConfigureAwait(false);
            return new Response(result);
        }
    }
}
