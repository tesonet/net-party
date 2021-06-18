using MediatR;
using PartyCli.Contracts.Response;

namespace PartyCli.Core.Commands
{
	public record GetHelpCommand : IRequest<ConsoleResponse>
	{
	}
}