using FluentValidation;
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
        private ILogger logger;
        public ILogger Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = new LoggerConfiguration().CreateLogger();
                }
                
                return logger;
            }
            set
            {
                logger = value;
            }
        }

        public IValidator<TRequest> Validator { get; set; }

        public async Task HandleAsync(TRequest request)
        {
            Guard.NotNull(request, nameof(request));

            Logger.Information("Started execute {0}...", typeof(TRequest).Name);

            ValidaterRequest(request);

            try
            {
                await HandleBaseAsync(request)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while executin {0}!", nameof(TRequest));
            }
        }

        private void ValidaterRequest(TRequest request)
        {
            var validationReult = Validator.Validate(request);

            if (!validationReult.IsValid)
            {
                throw new ValidationException(validationReult.Errors);
            }
        }

        public abstract Task HandleBaseAsync(TRequest request);
    }
}
