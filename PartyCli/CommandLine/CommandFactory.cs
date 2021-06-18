using System.Collections.Generic;
using MediatR;
using PartyCli.CommandLine.Parsing;
using PartyCli.Contracts.Exceptions;
using PartyCli.Contracts.Response;

namespace PartyCli.CommandLine
{
	public class CommandFactory : ICommandFactory
	{
		private readonly IEnumerable<ICommandParser<IRequest<ConsoleResponse>>> _commandParsers;

		public CommandFactory(IEnumerable<ICommandParser<IRequest<ConsoleResponse>>> commandParsers)
		{
			_commandParsers = commandParsers;
		}

		public IRequest<ConsoleResponse> Create(string[] args)
		{
			foreach (var commandParser in _commandParsers)
			{
				var command = commandParser.Parse(args);
				if (command is not null)
				{
					return command;
				}
			}

			throw new PartyCliException("Invalid arguments");
		}
	}
}