using System.Collections.Generic;
using AutoMapper;
using CommandLine;
using MediatR;
using PartyCli.Contracts.Response;

namespace PartyCli.CommandLine.Parsing
{
	public class CommandParser<TOptions, TCommand> : ICommandParser<TCommand> where TCommand : IRequest<ConsoleResponse>
	{
		private readonly IMapper _mapper;
		private readonly Parser _parser;

		public CommandParser(IMapper mapper, Parser parser)
		{
			_mapper = mapper;
			_parser = parser;
		}

		private T GetParsedOptions<T>(IEnumerable<string> args)
		{
			var parserResult = _parser.ParseArguments(args, typeof(T));
			var parsed = default(T);
			parserResult
				.WithParsed(obj => { parsed = (T) obj; })
				.WithNotParsed(_ => parsed = default /* Ideally we should handle this better */);

			return parsed;
		}

		public TCommand Parse(IEnumerable<string> args)
		{
			var options = GetParsedOptions<TOptions>(args);

			return options is null ? default : _mapper.Map<TCommand>(options);
		}
	}
}