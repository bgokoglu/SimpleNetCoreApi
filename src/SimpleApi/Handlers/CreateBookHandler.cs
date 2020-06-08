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
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Response>
    {
        private readonly IRepository<Book> _repository;

        public CreateBookHandler(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.Add(new Book { Title = request.Title }).ConfigureAwait(false);
            return new Response(book);
        }
    }
}
