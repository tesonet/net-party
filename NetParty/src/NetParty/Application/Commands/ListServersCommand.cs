using System;
using System.Collections.Generic;
using log4net;
using McMaster.Extensions.CommandLineUtils;
using NetParty.Domain.Servers;

namespace NetParty.Application.Commands
{
    public class ListServersCommand : ICommand
    {
        private readonly ILog _log = LogManager.GetLogger(nameof(ListServersCommand));

        private readonly IServerService _serverService;

        public ListServersCommand(IServerService serverService)
        {
            _serverService = serverService;
        }

        public void Attach(CommandLineApplication app)
        {
            app.Command("server_list", cmd =>
            {
                var doNotRefresh = cmd.Option("--local", "Fetch local server list and display server names and total number of servers.",
                    CommandOptionType.SingleOrNoValue);

                cmd.ShowInHelpText = true;

                cmd.Description =
                    "Fetch servers from API, store them locally and display server names and total number of servers";

                cmd.OnExecuteAsync(async cancelationToken =>
                {
                    _log.Debug("Listing servers..");

                    try
                    {
                        DisplayOutputInConsole(await _serverService.GetAll(!doNotRefresh.HasValue()));

                        return 0;
                    }
                    catch (Exception e)
                    {
                        _log.Error($"Couldn't refresh servers due to an exception: {e.Message}{Environment.NewLine}{e.StackTrace}");
                        
                        Output.Error($"Couldn't refresh servers due to an exception: {e.Message}");

                        return 1;
                    }
                });
            });
        }

        private static void DisplayOutputInConsole(ICollection<Server> servers)
        {
            Output.Info("Listing servers:");

            foreach (var server in servers)
            {
                Output.Info(server.Name);
            }

            Output.Info($"Total: {servers.Count}");
        }
    }
}