using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsApi.Application.Pipeline
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {RequestName} - {@Request}", typeof(TRequest).Name, request);

            try
            {
                var response = await next();
                _logger.LogInformation("Handled {RequestName}", typeof(TRequest).Name);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling {RequestName}: {Message}", typeof(TRequest).Name, ex.Message);
                throw;
            }
        }
    }
}
