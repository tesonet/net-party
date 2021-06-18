using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PartyCli.Core.Behaviors
{
	public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

		public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
		{
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var stopwatch = Stopwatch.StartNew();
			_logger.LogInformation("Executing: {Name}", request.GetType().Name);

			var response = await next();

			_logger.LogInformation("Finished executing: {Name}. Took {Time}ms", request.GetType().Name, stopwatch.Elapsed.TotalMilliseconds);

			return response;
		}
	}
}