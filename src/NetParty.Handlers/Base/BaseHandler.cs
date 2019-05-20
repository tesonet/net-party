using GuardNet;
using NetParty.Contracts.Requests.Base;
using Serilog;
using System;
using System.Threading.Tasks;

namespace NetParty.Handlers.Base
{
    public abstract class BaseHandler<TRequest> : IHandler<TRequest>
        where TRequest : BaseRequest
    {
        public ILogger Logger { get; set; }

        public async Task HandleAsync(TRequest request)
        {
            Guard.NotNull(request, nameof(request));

            Logger.Information("Started execute {0}...", typeof(TRequest).Name);

            try
            {
                await HandleBaseAsync(request)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error while executin {0}!", nameof(TRequest));
            }
        }

        public abstract Task HandleBaseAsync(TRequest request);
    }
}
