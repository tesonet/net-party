using MediatR;
using PartyCli.Contracts.Response;

namespace PartyCli.CommandLine
{
	public interface ICommandFactory
	{
		IRequest<ConsoleResponse> Create(string[] args);
	}
}