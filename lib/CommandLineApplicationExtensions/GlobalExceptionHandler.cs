namespace McMaster.Extensions.Hosting.CommandLine.Custom
{
    using System;
    using CommandLine;
    using Microsoft.Extensions.Logging;

    internal class GlobalExceptionHandler : IUnhandledExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public void HandleException(Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception.");
        }
    }
}