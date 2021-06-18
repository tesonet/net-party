using System;

namespace PartyCli.Contracts.Response
{
	public record ConsoleLine (string Text, ConsoleColor Color = ConsoleColor.White);
}