using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using partycli.Models;

namespace partycli
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();



        static void Main(string[] args)
        {
            var currentState = States.none;
             string name = null;
            int argIndex = 1;

            foreach (string arg in args)
            {
                if (currentState == States.none)
                {
                    if (arg == Commands.serverList)
                    {
                        currentState = States.server_list;
                        if (argIndex >= args.Count())
                        {
                            var token = GetAuthorizationTokenAsync().Result;
                            var serverList = GetNewServerListAsync(token).Result;
                            StoreValue(ConfigConstants.serverList, serverList, false);
                            Log("Saved new server list: " + serverList);
                            DisplayList(serverList);
                        }
                    }
                    if (arg == Commands.config)
                    {
                        currentState = States.config;
                    }
                }
                else if (currentState == States.config)
                {
                    if (name == null)
                    {
                        name = arg;
                    }
                    else
                    {
                        StoreValue(ProccessName(name), arg);
                        Log("Changed " + ProccessName(name) + " to " + arg);
                        name = null;
                    }
                }
                else if (currentState == States.server_list)
                {
                    if (arg == Commands.local)
                    {
                        if (!String.IsNullOrEmpty(Properties.Settings.Default.serverlist)) { 
                        DisplayList(Properties.Settings.Default.serverlist);
                        } else
                        {
                            Console.WriteLine("Error: There are no server data in local storage");
                            Environment.Exit(0);
                        }
                    }
                }
                argIndex = argIndex + 1;
            }

            if(currentState == States.none)
            {
                Console.WriteLine("To set username and password, use command: partycli.exe config --username \"YOUR USERNAME\" --password \"YOUR PASSWORD\" ");
                Console.WriteLine("To renew list of servers, use command: partycli.exe server_list");
                Console.WriteLine("To see saved list of servers, use command: partycli.exe server_list --local ");
                Environment.Exit(0);
            }
            else if (currentState == States.config) {
                if (name != null)
                {
                    Console.WriteLine("Error: Couldn't save " + ProccessName(name) + ", no value was found. Check if command was input correctly.");
                    Environment.Exit(0);
                }
            }
        }

        static void StoreValue(string name, string value, bool writeToConsole = true)
        {
            try { 
                var settings = Properties.Settings.Default;
                settings[name] = value;
                settings.Save();
                if (writeToConsole) { 
                Console.WriteLine("Changed " + name + " to " + value);
                }
            }
            catch {
                Console.WriteLine("Error: Couldn't save " + name + ". Check if command was input correctly." );
                Environment.Exit(0);
            }

        }

        static string ProccessName(string name)
        {
            name = name.Replace("-", string.Empty);
            return name;
        }

        static async Task<TokenModel> GetAuthorizationTokenAsync()
        {
            var body = new Dictionary<string, string>
            {
                {ConfigConstants.username, Properties.Settings.Default.username },
                {ConfigConstants.password, Properties.Settings.Default.password }
            };
            var content = new FormUrlEncodedContent(body);
            var response = await client.PostAsync(Constants.tokenUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error: " + response.StatusCode + " " + response.RequestMessage);
                Environment.Exit(0);
            }
            var responseString = await response.Content.ReadAsStringAsync();
            var tokenObject = JsonConvert.DeserializeObject<TokenModel>(responseString);
            Log("Got new token");
            return tokenObject;
        }

        static async Task<string> GetNewServerListAsync(TokenModel token)
        {
                var request = new HttpRequestMessage(HttpMethod.Get, Constants.serverUrl);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                     Console.WriteLine("Error: " + response.StatusCode + " " + response.RequestMessage);
                     Environment.Exit(0);
                 }
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
        }

        static void DisplayList(string serverListString)
        {
            var serverlist = JsonConvert.DeserializeObject<List<ServerModel>>(serverListString);
            Console.WriteLine("Server list: ");
            for (var index = 0; index < serverlist.Count; index++)
            {
                Console.WriteLine("Name: " + serverlist[index].Name);
            }
            Console.WriteLine("Total servers: " + serverlist.Count);
        }

        static void Log(string action)
        {
            var newLog = new LogModel
            {
                Action = action,
                Time = DateTime.Now
            };
            List<LogModel> currentLog;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.log))
            {
                currentLog = JsonConvert.DeserializeObject<List<LogModel>>(Properties.Settings.Default.log);
                currentLog.Add(newLog);
            }
            else
            {
                currentLog = new List<LogModel> { newLog };
            }

            StoreValue(ConfigConstants.log, JsonConvert.SerializeObject(currentLog), false);
        }
    }


}
