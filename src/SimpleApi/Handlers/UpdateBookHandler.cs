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
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Response>
    {
        private readonly IRepository<Book> _repository;

        public UpdateBookHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.Update(new Book { Id = request.Id, Title = request.Title }).ConfigureAwait(false);
            return new Response(book);
        }
    }
}
