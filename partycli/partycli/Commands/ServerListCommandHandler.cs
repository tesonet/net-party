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
    class ServerListCommandHandler : AbstractCommandHandler, ICommandHandler<ServerListCommand>
    {
        public ServerListCommandHandler(IExecutor executor, ILogger logger, IConsoleWriter writer) 
            : base(executor, logger, writer)
        {

        }
        public void Handle(ServerListCommand command)
        {
            writer.Write($"Fetching {(command.FetchLocal ? "local" : "")} server list...");
            logger.Info($"Fetching {(command.FetchLocal ? "local" : "")} server list...");

            try
            {
                IEnumerable<Server> servers = null;

                Task.Run(async () =>
                {
                    servers = await executor.FetchServers(command.FetchLocal);
                }).Wait();

                writer.Write("Fetching server list. Complete!");
                logger.Info("Fetching server list. Complete!");
                writer.Write(servers);
            }
            catch (Exception ex)
            {
                writer.Write("Error fetching servers. See log for more info.");
                logger.Error(ex.Message, ex);
            }
        }
    }
}
