namespace PartyCli.CommandLine
{
	public class CommandLineArgumentsAccessor : ICommandLineArgumentsAccessor
	{
		public string[] Arguments { get; }

		public CommandLineArgumentsAccessor(string[] args)
		{
			Arguments = args;
		}
	}
}