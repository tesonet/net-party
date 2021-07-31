using System;
using System.Collections.Generic;
using CommandLine;

namespace PartyCLI
{
    class Program
    {
        private static UnityContainerManager unityContainer = new UnityContainerManager();

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<UserData, LocalServerList>(args)
                .WithParsed<UserData>(options => SaveUserData(options))
                .WithParsed<LocalServerList>(options => GetServersList(options));
        }

        static void SaveUserData(UserData user)
        {
            Console.WriteLine($"Saving user data: {user.Username}, {user.Password}");
            unityContainer.GetDataFilesManager().SaveUserData(user);
        }

        static void GetServersList(LocalServerList local)
        {
            List<Server> sList;

            if (!local.ShowLocal)
            {
                var token = unityContainer.GetServerRequestsManager().GetTokenAsync(unityContainer.GetDataFilesManager().GetUserData()).Result;
                sList = unityContainer.GetServerRequestsManager().GetServersAsync(token.Token).Result;
                unityContainer.GetDataFilesManager().SaveServersList(sList);
            }
            else
            {
                sList = unityContainer.GetDataFilesManager().GetServersList();
                if(sList == null)
                    Console.WriteLine("Error: Local file empty. Try calling \'server_list\' without \'--local\' param first.");
            }

            if (sList != null && sList.Count >= 0)
                unityContainer.GetDisplayManager().Display(sList);
        }
    }
}
