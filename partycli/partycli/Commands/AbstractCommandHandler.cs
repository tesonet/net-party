using Autofac;
using log4net;
using partycli.core.Execution;
using partycli.Presentation;
using System.Threading.Tasks;

namespace partycli.Commands
{
    public abstract class AbstractCommandHandler
    {
        protected IExecutor executor;
        protected IConsoleWriter writer;
        protected ILog logger;

        public AbstractCommandHandler(IExecutor executor, IConsoleWriter writer)
        {
            this.executor = executor;
            this.writer = writer;
            logger = LogManager.GetLogger(GetType());
        }

        public static async Task StartWork(ICommand command, IContainer container)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                if (command != null)
                {
                    if (command.GetType() == typeof(ConfigCommand))
                    {
                        await scope.Resolve<ICommandHandler<ConfigCommand>>().Handle((ConfigCommand)command);
                    }

                    if (command.GetType() == typeof(ServerListCommand))
                    {
                        await scope.Resolve<ICommandHandler<ServerListCommand>>().Handle((ServerListCommand)command);
                    }
                }
            }
        }
    }
}
