using MediatR;
using PartyCli.Contracts.Response;

namespace PartyCli.Commands
{
	public record ParseCommandLineCommand : IRequest<IRequest<ConsoleResponse>>;
}