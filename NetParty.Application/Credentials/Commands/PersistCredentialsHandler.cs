using MediatR;
using NetParty.Application.Extensions;
using NetParty.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NetParty.Application.Credentials.Commands
{
    public class PersistCredentialsHandler : IRequestHandler<PersistCredentialsCommand, ICommandResult>
    {
        private IPersistance _persistanceService;
        private PersistCredentialsResponse _result;
        public PersistCredentialsHandler(IPersistance persistanceService)
        {
            _persistanceService = persistanceService;
            _result = new PersistCredentialsResponse();
        }

        public async Task<ICommandResult> Handle(PersistCredentialsCommand request, CancellationToken cancellationToken)
        {
            await _persistanceService.SaveCredentials(request.Credentials.MapToEntity());
            return _result;
        }
    }
}
