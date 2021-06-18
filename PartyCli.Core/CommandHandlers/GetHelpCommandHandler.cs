using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PartyCli.Contracts.Response;
using PartyCli.Core.Commands;

namespace PartyCli.Core.CommandHandlers
{
	public class GetHelpCommandHandler : IRequestHandler<GetHelpCommand, ConsoleResponse>
	{
		public Task<ConsoleResponse> Handle(GetHelpCommand request, CancellationToken cancellationToken)
		{
			var response = new ConsoleResponse
			{
				Lines = new[]
				{
					new ConsoleLine(@"░█▀▀▄░░░░░░░░░░░▄▀▀█
░█░░░▀▄░▄▄▄▄▄░▄▀░░░█
░░▀▄░░░▀░░░░░▀░░░▄▀
░░░░▌░▄▄░░░▄▄░▐▀▀
░░░▐░░█▄░░░▄█░░▌▄▄▀▀▀▀█
░░░▌▄▄▀▀░▄░▀▀▄▄▐░░░░░░█
▄▀▀▐▀▀░▄▄▄▄▄░▀▀▌▄▄▄░░░█
█░░░▀▄░█░░░█░▄▀░░░░█▀▀▀
░▀▄░░▀░░▀▀▀░░▀░░░▄█▀
░░░█░░░░░░░░░░░▄▀▄░▀▄
░░░█░░░░░░░░░▄▀█░░█░░█
░░░█░░░░░░░░░░░█▄█░░▄▀
░░░█░░░░░░░░░░░████▀
░░░▀▄▄▀▀▄▄▀▀▄▄▄█▀", ConsoleColor.DarkYellow),
					new ConsoleLine("NO HELP!", ConsoleColor.Red)
				}
			};

			return Task.FromResult(response);
		}
	}
}