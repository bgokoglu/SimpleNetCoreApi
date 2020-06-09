using MediatR;
using Microsoft.Extensions.Logging;
using SimpleAPI.Core;
using SimpleAPI.Domain;
using SimpleAPI.Queries;
using SimpleAPI.Repositories.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, Response>
    {
        private readonly IRepository<Book> _repository;
        private readonly ILogger<GetAllBooksHandler> _logger;

        public GetAllBooksHandler(IRepository<Book> repository, ILogger<GetAllBooksHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("retrieving books");

            var books = await _repository.GetAll().ConfigureAwait(false);

            _logger.LogInformation($"retrieved {books?.Count} books");

            return new Response(books);
        }
    }
}
