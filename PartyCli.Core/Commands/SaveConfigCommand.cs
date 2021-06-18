using MediatR;
using PartyCli.Contracts.Response;

namespace PartyCli.Core.Commands
{
	public record SaveConfigCommand(string UserName, string Password) : IRequest<ConsoleResponse>
	{
	}
}