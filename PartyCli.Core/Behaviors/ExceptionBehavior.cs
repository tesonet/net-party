using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PartyCli.Contracts.Exceptions;
using PartyCli.Contracts.Response;

namespace PartyCli.Core.Behaviors
{
	public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : class
	{
		private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> _logger;

		public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
		{
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			try
			{
				return await next();
			}
			catch (PartyCliException e)
			{
				_logger.LogError("Error while executing {Name}", request.GetType().Name);

				if (typeof(TResponse) == typeof(ConsoleResponse)) //Not the most elegant way, but it will do in this case : )
				{
					return ConsoleResponse.FromError(e) as TResponse;
				}

				throw;
			}
		}
	}
}