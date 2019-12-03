using CommandLine;
using partycli4.Data;
using partycli4.Interface;
using System;
using System.Collections;
using System.Collections.Generic;

namespace partycli4
{
    class Program
    {
        private static readonly IDataRepository _dataRepository;
        private static readonly IProvideData _provideData;
        static void Main(string[] args)
        {

            try
            {               
                switch (args[0].ToLower())
                {
                    case "config":
                        Parser.Default.ParseArguments<User>(args)
                            .WithParsed(Config)
                            .WithNotParsed(ParsingProblem);
                        break;
                    case "server_list":
                        Parser.Default.ParseArguments<LocalServer>(args)
                            .WithParsed(Serverlist)
                            .WithNotParsed(ParsingProblem);
                        break;
                    default:
                        InvalidCommand();
                        break;

                }
            }
            catch
            {
                Console.WriteLine("Command Error");
            }
        }
        private static void Config(User user)
        {
            _dataRepository.SaveUser(user);
            Console.WriteLine("User credentials saved");
        }

        private static void Serverlist(LocalServer Local)
        {
            IList<Server> list;
            if (Local.IsLocal)
            {
                list = _dataRepository.GetServerList();
                if (list == null)
                {
                    Console.WriteLine("Empty");
                }
            }
            else
            {
                var user = _dataRepository.GetUser();
                var token = GetAccessToken(user);
                list = _provideData.GetServerList(token);
                _dataRepository.SaveServerList(list);
            }

            foreach (var server in list)
            {
                Console.WriteLine($"Server: {server.Name}");
            }
        }
        private static string GetAccessToken(User user)
        {

            try
            {
                return _provideData.GetGeneratedToken(user);
            }
            catch
            {
                Console.WriteLine("User credentials are invalid!");
            }
            return null;
        }
        private static void ParsingProblem(IEnumerable errors)
        {
            Console.WriteLine("Error");
        }

        private static void InvalidCommand()
        {
            Console.WriteLine("Invalid command!");
            Console.WriteLine("Available commands: ");
            Console.WriteLine(" config --username --password");
            Console.WriteLine(" server_list");
            Console.WriteLine(" server_list --local");
        }


    }

}
