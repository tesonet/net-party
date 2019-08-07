using System;
using System.Threading.Tasks;
using NetParty.CLI.Controllers;
using NetParty.CLI.ResultsPrinter;
using NetParty.Contracts.Results;
using NetParty.Domain.Exceptions;
using NLog;

namespace NetParty.CLI.Utils
{
    public class ExceptionHandlingDecorator<T> : IController<T>
    {
        private readonly IController<T> _decorated;
        private readonly IResultsPrinter _resultsPrinter;
        private readonly Logger _logger;

        public ExceptionHandlingDecorator(IController<T> decorated, IResultsPrinter resultsPrinter, Logger logger)
        {
            _decorated = decorated;
            _resultsPrinter = resultsPrinter;
            _logger = logger;
        }

        public async Task Handle(T options)
        {
            try
            {
                await _decorated.Handle(options);
            }
            catch (DomainException domainException)
            {
                var error = new Error {Reason = domainException.Reason, Message = domainException.Message};

                _logger.Error($"[{error.Reason}] {error.Message}");
                _resultsPrinter.Print(error);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
                _resultsPrinter.Print(Error.Default);
            }
        }
    }
}