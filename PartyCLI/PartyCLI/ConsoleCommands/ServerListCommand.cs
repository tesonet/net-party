namespace PartyCLI.ConsoleCommands
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using ManyConsole;

    using PartyCLI.ApiConfigurations;
    using PartyCLI.ApiProviders;
    using PartyCLI.ConsoleOutputWriters;
    using PartyCLI.ConsoleOutputWriters.Parameters;
    using PartyCLI.Data.Models;
    using PartyCLI.Data.Repositories;
    using PartyCLI.Models.Responses;

    public class ServerListCommand : ConsoleCommand
    {
        private readonly IServerListOutputWriter outputWriter;
        private readonly IGenericRepository repository;
        private readonly ApiProvider apiProvider;
        private readonly IApiConfiguration apiConfig;
        private bool isLocal;

        public ServerListCommand(IServerListOutputWriter outputWriter, IGenericRepository repository, ApiProvider apiProvider, IApiConfiguration apiConfig)
        {
            IsCommand("server_list", "Fetches servers from API, stores them in persistent data store and displays server names and total number of servers in the console.");
            HasOption("local", "Fetches servers from persistent data store instead of API.", l => isLocal = true);

            this.outputWriter = outputWriter;
            this.repository = repository;
            this.apiProvider = apiProvider;
            this.apiConfig = apiConfig;
        }

        public override int Run(string[] remainingArguments)
        {
            Console.WriteLine($@"Fetching server list, please wait...{Environment.NewLine}");

            if (isLocal)
            {
                DisplayServersFromStorage();
            }
            else
            {
                var authParams = new Dictionary<string, string>
                                     {
                                         { "username", apiConfig.Username },
                                         { "password", apiConfig.Password }
                                     };

                var response = apiProvider.Post<TokenResponse>("tokens", authParams);
                var headers = new Dictionary<string, string> { { "Authorization", $"Bearer {response.Token}" } };
                var serverList = apiProvider.Get<List<ServerResponse>>("servers", null, headers);
                var serverEntityList = serverList.Select(server => new Server { Distance = server.Distance, Name = server.Name }).ToList();
                var serverIds = repository.GetAll<Server>().Select(es => es.Id).ToList();

                UpdateRecordsInStorage(serverIds, serverEntityList);

                OutputServers(serverEntityList);
            }

            if (Process.GetCurrentProcess().ProcessName != Constants.TestHostProcessName)
            {
                Console.ReadKey();
            }

            return 0;
        }

        public void DisplayServersFromStorage()
        {
            var servers = repository.GetAll<Server>().ToList();

            OutputServers(servers);
        }

        public void UpdateRecordsInStorage(List<int> existingServerIds, List<Server> newServers)
        {
            using var ts = repository.GetTransactionScope();

            foreach (var existingServerId in existingServerIds)
            {
                repository.Delete<Server>(existingServerId);
            }

            repository.AddRange(newServers);

            ts.Commit();
        }

        private void OutputServers(IList<Server> servers)
        {
            if (servers.Any())
            {
                var parameters = new ServerListConsoleOutputParams
                {
                    ForegroundColor = ConsoleColor.Yellow
                };

                outputWriter.WriteLineToConsole(parameters, servers.ToArray());
            }
            else
            {
                Console.WriteLine($@"No servers found.{Environment.NewLine}");
            }

            Console.WriteLine(Constants.ExitMessage);
        }
    }
}