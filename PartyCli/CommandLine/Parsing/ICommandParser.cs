using System.Collections.Generic;

namespace PartyCli.CommandLine.Parsing
{
	public interface ICommandParser<out TCommand>
	{
		TCommand Parse(IEnumerable<string> args);
	}
}