using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PartyCli.CommandLine;
using PartyCli.Commands;
using PartyCli.Contracts.Response;

namespace PartyCli.CommandHandlers
{
	public class ParseCommandLineCommandHandler : IRequestHandler<ParseCommandLineCommand, IRequest<ConsoleResponse>>
	{
		private readonly ICommandLineArgumentsAccessor _commandLineArgumentsAccessor;
		private readonly ICommandFactory _commandFactory;

		public ParseCommandLineCommandHandler(ICommandLineArgumentsAccessor commandLineArgumentsAccessor, ICommandFactory commandFactory)
		{
			_commandLineArgumentsAccessor = commandLineArgumentsAccessor;
			_commandFactory = commandFactory;
		}

		public Task<IRequest<ConsoleResponse>> Handle(ParseCommandLineCommand request, CancellationToken cancellationToken)
		{
			var args = _commandLineArgumentsAccessor.Arguments;

			var command = _commandFactory.Create(args);

			return Task.FromResult(command);
		}
	}
}