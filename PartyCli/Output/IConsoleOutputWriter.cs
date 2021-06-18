using PartyCli.Contracts.Response;

namespace PartyCli.Output
{
	public interface IConsoleOutputWriter
	{
		void Write(ConsoleResponse response);
	}
}