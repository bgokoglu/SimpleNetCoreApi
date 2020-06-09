using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleAPI.PipelineBehaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");

            var response = await next().ConfigureAwait(false);

            _logger.LogInformation($"Handled {typeof(TRequest).Name}");

            return response;
        }
    }
}
