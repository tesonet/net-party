using partycli.core.Execution;
using partycli.core.Repositories.Model;
using partycli.Presentation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace partycli.Commands
{
    public class ServerListCommandHandler : AbstractCommandHandler, ICommandHandler<ServerListCommand>
    {
        public ServerListCommandHandler(IExecutor executor, IConsoleWriter writer) 
            : base(executor, writer)
        {

        }
        public async Task Handle(ServerListCommand command)
        {
            writer.Write($"Fetching {(command.FetchLocal ? "local" : "")} server list...");
            logger.Info($"Fetching {(command.FetchLocal ? "local" : "")} server list...");

            try
            {
                IEnumerable<Server> servers = servers = await executor.FetchServers(command.FetchLocal);
                
                writer.Write("Fetching server list. Complete!");
                logger.Info("Fetching server list. Complete!");
                writer.Write(servers);
            }
            catch (Exception e)
            {
                //Catch all unhandled exceptions
                logger.Error($"Failed to fetch servers with params: {(command.FetchLocal ? "--local" : "")}.");
                logger.Debug(e.Message, e);
                writer.Write("Error fetching servers. See log for more info.");
            }
        }
    }
}
