using System;
using Castle.DynamicProxy;
using NLog;

namespace NetParty.CLI.Utils
{
    public class LoggingInterceptor : IInterceptor
    {
        private readonly Logger _logger;

        public LoggingInterceptor(Logger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var invocationId = Guid.NewGuid();

            LogInvocationStarted(invocation, invocationId);
            invocation.Proceed();
            LogInvocationFinished(invocation, invocationId);
        }

        private void LogInvocationStarted(IInvocation invocation, Guid invocationId)
        {
            var invocationStartedEvent = new
            {
                InvocationId = invocationId,
                Message = "Method invocation started",
                MethodName = invocation.Method.Name,
                InvocationTarget = invocation.InvocationTarget,
                Arguments = invocation.Arguments
            };
            _logger.Info(invocationStartedEvent);
        }

        private void LogInvocationFinished(IInvocation invocation, Guid invocationId)
        {
            var invocationFinishedEvent = new
            {
                InvocationId = invocationId,
                Message = "Method invocation finished",
                MethodName = invocation.Method.Name,
            };
            _logger.Info(invocationFinishedEvent);
        }
    }
}