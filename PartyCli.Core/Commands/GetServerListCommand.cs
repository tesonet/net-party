using MediatR;
using PartyCli.Contracts.Response;

namespace PartyCli.Core.Commands
{
	public record GetServerListCommand (bool UseLocal) : IRequest<ConsoleResponse>
	{
	}
}