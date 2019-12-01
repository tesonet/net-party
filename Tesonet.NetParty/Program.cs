using System;
using System.Collections;
using System.Collections.Generic;
using log4net;
using CommandLine;
using Tesonet.NetParty.Models.Interfaces;
using Tesonet.NetParty.Models.Models;

namespace Tesonet.NetParty
{
	class Program
	{
		private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly UnityContainerManager _unityContainer = new UnityContainerManager();

		private static IDataRepository _dataRepository;
		private static IDataProvider _dataProvider;

		static void Main(string[] args)
		{
			try
			{
				init(args);

				// Check if args exists
				if (args.Length == 0)
				{
					showInvalidCommandMessage();
					return;
				}
				// Switch to command
				switch (args[0].ToLower())
				{
					case "config":
						Parser.Default.ParseArguments<User>(args)
							.WithParsed(configCommand)
							.WithNotParsed(handleParseError);
						break;
					case "server_list":
						Parser.Default.ParseArguments<ServerListCommandArgs>(args)
							.WithParsed(serverListCommand)
							.WithNotParsed(handleParseError);
						break;
					default:
						showInvalidCommandMessage();
						break;
				}
			}
			catch (Exception e)
			{
				_log.Error(e);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Unexpected error occured. For more details please check log file.");
			}
		}

		private static void init(string[] args)
		{
			_log.Info("Console app was started!");
			_log.Info($"Arguments: {string.Join(", ", args)}");
			_dataProvider = _unityContainer.GetDataProvider();
			_dataRepository = _unityContainer.GetDataRepository();
		}

		private static void configCommand(User user)
		{
			Console.WriteLine("Validating your credentials...");
			if (string.IsNullOrEmpty(getAccessToken(user)))
				return;
			Console.WriteLine("Saving your credentials...");
			_dataRepository.SaveUser(user);
			Console.WriteLine("User credentials were saved!");
		}

		private static void serverListCommand(ServerListCommandArgs args)
		{
			IList<Server> list;
			if (args.IsLocal)
			{
				// Get local server list
				list = _dataRepository.GetServerList();
				if (list == null)
				{
					_log.Info("There is no data stored locally!");
					return;
				}
			}
			else
			{
				// Get and update server list
				var user = _dataRepository.GetUser();
				var token = getAccessToken(user);
				if (string.IsNullOrEmpty(token))
					return;
				list = _dataProvider.GetServiceList(token);
				_dataRepository.SaveServerList(list);
			}

			// Show server list
			foreach (var server in list)
			{
				showServer(server);
			}
		}

		private static void showServer(Server server)
		{
			Console.WriteLine($"Server: {server.Name,-20} distance: {server.Distance,-15}");
		}

		private static void showInvalidCommandMessage()
		{
			Console.WriteLine("Please enter valid command!");
			Console.WriteLine("Available command lines:");
			Console.WriteLine("	config --username \"YOUR USERNAME\" --password \"YOUR PASSWORD\"");
			Console.WriteLine("	server_list");
			Console.WriteLine("	server_list --local");
		}

		private static string getAccessToken(User user)
		{
			if (user == null || string.IsNullOrEmpty(user.Username) ||string.IsNullOrEmpty(user.Password))
				Console.WriteLine("Please enter valid user credentials!");
			try
			{
				return _dataProvider.GetAuthenticationToken(user);
			}
			catch (UnauthorizedAccessException e)
			{
				_log.Error(e);
				Console.WriteLine("User credentials are invalid!");
			}
			return null;
		}

		private static void handleParseError(IEnumerable errs)
		{
			_log.Info("Command Line parameters were not provided or invalid!");
		}
	}
}
