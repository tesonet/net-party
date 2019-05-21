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
        private ILogger _logger;
        public ILogger Logger
        {
            get => _logger ?? (_logger = new LoggerConfiguration().CreateLogger());
            set => _logger = value;
        }

        public IValidator<TRequest> Validator { get; set; }

        public async Task HandleAsync(TRequest request)
        {
            Guard.NotNull(request, nameof(request));

            Logger.Debug("Started execute {0}...", typeof(TRequest).Name);

            RequestValidator(request);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                await HandleBaseAsync(request)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while execution {0}!", nameof(TRequest));
            }
            finally
            {
                watch.Stop();

                _logger.Debug($"Time taken to execute {typeof(TRequest).Name}: {watch.Elapsed.TotalMilliseconds}ms");
            }
        }

        private void RequestValidator(TRequest request)
        {
            var validationResult = Validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public abstract Task HandleBaseAsync(TRequest request);
    }
}
