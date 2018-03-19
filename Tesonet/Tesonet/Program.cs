using System;
using System.Linq;

namespace Tesonet
{
    class Program
    {
        private static ITesonetService _tesonetService;
        private static IFileService _fileService;
        private static ILogService _logService;

        static void Main(string[] args)
        {
            Configurations.Start();
            _tesonetService = Configurations.container.GetInstance<ITesonetService>();
            _fileService = Configurations.container.GetInstance<IFileService>();
            _logService = Configurations.container.GetInstance<ILogService>();

            _logService.Log("Program started. All services are initialized");

            if (args.Length == 5 && args[0] == "config" && args[1] == "--username" && args[3] == "--password" && !string.IsNullOrEmpty(args[2]) && !string.IsNullOrEmpty(args[4]))
            {
                _logService.Log("Username and password are being saved to data store.");
                _fileService.WriteUserData(username: args[2], password: args[4]);
                _logService.Log("Username and password stored to data store.");
            }
            else if (args[0] == "server_list" && args.Length == 1)
            {
                _logService.Log("Fetching user data from data store.");
                string[] userData = null;
                try
                {
                    userData = _fileService.ReadUserData();
                }
                catch(Exception ex)
                {
                    _logService.Log(ex.Message);
                    _logService.Log("---Program END---");
                    return;
                }

                _logService.Log("Getting user access token");
                var token = _tesonetService.GetAccessToken(username: userData[0], password: userData[1]);

                _logService.Log("Getting server list");
                var servers = _tesonetService.GetServerList(token).ToList();

                _logService.Log("Writing server list to file");
                _fileService.WriteServerData(servers);

                _logService.Log("Writing server list and count to console");
                Console.WriteLine("Total number of servers: {0}", servers.Count);
                foreach (var server in servers)
                {
                    Console.WriteLine(server.Name);
                }
            }
            else if (args[0] == "server_list" && args[1] == "--local")
            {
                _logService.Log("Reading servers from file");
                string[] localServers = null;
                try
                {
                    localServers = _fileService.ReadLocalServers();
                }
                catch(Exception ex)
                {
                    _logService.Log(ex.Message);
                    _logService.Log("---Program END---");
                    return;
                }

                _logService.Log("Writing server list and count to console");
                Console.WriteLine("Total number of servers: {0}", localServers.Length);
                foreach (var localServer in localServers)
                {
                    Console.WriteLine(localServer);
                }
            }
            else
            {
                _logService.Log("Unkown command was given... Please try again.");
            }
            _logService.Log("---Program END---");

            Console.ReadKey();
        }
    }
}
