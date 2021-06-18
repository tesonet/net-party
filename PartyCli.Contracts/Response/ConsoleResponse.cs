using System;
using System.Collections.Generic;
using PartyCli.Contracts.Exceptions;

namespace PartyCli.Contracts.Response
{
	public record ConsoleResponse(ICollection<ConsoleLine> Lines)
	{
		public ConsoleResponse() : this(new List<ConsoleLine>())
		{
		}

		public static ConsoleResponse Empty => new();

		public static ConsoleResponse FromError(PartyCliException cliException)
		{
			var response = new ConsoleResponse();
			response.Lines.Add(new ConsoleLine(cliException.Message, ConsoleColor.Red));
			return response;
		}
	}
}