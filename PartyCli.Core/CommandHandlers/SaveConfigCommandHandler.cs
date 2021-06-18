using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PartyCli.Contracts.Models;
using PartyCli.Contracts.Response;
using PartyCli.Core.Commands;
using PartyCli.Persistence;

namespace PartyCli.Core.CommandHandlers
{
	public class SaveConfigCommandHandler : IRequestHandler<SaveConfigCommand, ConsoleResponse>
	{
		private readonly IConfigRepository _repository;

		public SaveConfigCommandHandler(IConfigRepository repository)
		{
			_repository = repository;
		}

		public async Task<ConsoleResponse> Handle(SaveConfigCommand request, CancellationToken cancellationToken)
		{
			await _repository.Save(new Config(request.UserName, request.Password));

			return ConsoleResponse.Empty;
		}
	}
}