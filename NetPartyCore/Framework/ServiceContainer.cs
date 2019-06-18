using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetPartyCore.Framework
{
    class ServiceContainer
    {

        public ServiceContainer()
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder => loggingBuilder.AddConsole())
                //.AddSingleton<IInputParser, InputParser>()
                .BuildServiceProvider();

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger<Program>();
        }

    }
}
