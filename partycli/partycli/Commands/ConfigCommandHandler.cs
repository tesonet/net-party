using partycli.core.Execution;
using partycli.core.Logging;
using partycli.core.Repositories.Model;
using partycli.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Commands
{
    class ConfigCommandHandler : AbstractCommandHandler, ICommandHandler<ConfigCommand>
    {
        public ConfigCommandHandler(IExecutor executor, ILogger logger, IConsoleWriter writer) 
            : base (executor, logger, writer)
        {

        }
        public void Handle(ConfigCommand command)
        {
            writer.Write("Saving credentials...");
            logger.Info("Saving credentials...");

            try
            {
                writer.Write($"Config with options: {command.Username} {command.Password}");
                executor.SaveCredentials(new Credentials() { Username = command.Username, Password = command.Password });
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                writer.Write("Saving credentials failed. See log for more info.");
            }
            logger.Info("Credentials saved!");
        }
    }
}
