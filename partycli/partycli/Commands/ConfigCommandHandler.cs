using partycli.core.Execution;
using partycli.core.Repositories.Model;
using partycli.Presentation;
using System;
using System.Threading.Tasks;

namespace partycli.Commands
{
    class ConfigCommandHandler : AbstractCommandHandler, ICommandHandler<ConfigCommand>
    {
        public ConfigCommandHandler(IExecutor executor, IConsoleWriter writer) 
            : base (executor, writer)
        {

        }
        public async Task Handle(ConfigCommand command)
        {
            try
            {
                writer.Write("Saving credentials...");
                logger.Info("Saving credentials...");
                
                executor.SaveCredentials(new Credentials() { Username = command.Username, Password = command.Password });

                writer.Write("Credentials saved!");
                logger.Info("Credentials saved!");
            }
            catch (Exception e)
            {
                //Catch all unhandled exceptions
                logger.Error($"Saving credentials failed with params username=\"{command.Username}\" password=\"{command.Password}\".");
                logger.Debug(e.Message, e);
                writer.Write("Saving credentials failed. See log for more info.");
            }
        }
    }
}
