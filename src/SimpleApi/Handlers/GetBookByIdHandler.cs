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
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Response>
    {
        private readonly IRepository<Book> _repository;
        private readonly ILogger<GetBookByIdHandler> _logger;

        public GetBookByIdHandler(IRepository<Book> repository, ILogger<GetBookByIdHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"retrieving book {request.BookId}");

            var book = await _repository.GetById(request.BookId).ConfigureAwait(false);

            _logger.LogInformation(book != null ? $"retrieved book {request.BookId}" : $"not found book {request.BookId}");

            return new Response(book);
        }
    }
}
