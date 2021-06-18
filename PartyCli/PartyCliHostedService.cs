using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using PartyCli.Commands;
using PartyCli.Output;

namespace PartyCli
{
	public class PartyCliHostedService : IHostedService
	{
		private readonly IMediator _mediator;
		private readonly IConsoleOutputWriter _outputWriter;


		public PartyCliHostedService(IMediator mediator, IConsoleOutputWriter outputWriter)
		{
			_mediator = mediator;
			_outputWriter = outputWriter;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			var command = await _mediator.Send(new ParseCommandLineCommand(), cancellationToken);
			var commandResponse = await _mediator.Send(command, cancellationToken);

			_outputWriter.Write(commandResponse);
		}

		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
	}
}