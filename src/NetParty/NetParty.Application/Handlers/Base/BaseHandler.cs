using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetParty.Application.Handlers.Base
{
    public abstract class BaseHandler<TRequest> : IHandler<TRequest>
        where TRequest : class
    {
        public ILogger Logger { get; set; }

        public async Task HandleAsync(TRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            Logger.Information("Started execute {0}...", nameof(TRequest));

            try
            {
                await HandleBaseAsync(request)
                    .ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Logger.Error(ex, "Error while executin {0}!", nameof(TRequest));
            }
        }

        public abstract Task HandleBaseAsync(TRequest request);
    }
}
