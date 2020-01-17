namespace PartyCLI.Tests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using PartyCLI.ApiProviders;
    using PartyCLI.ConsoleCommands;
    using PartyCLI.ConsoleOutputWriters;
    using PartyCLI.ConsoleOutputWriters.Parameters;
    using PartyCLI.Data.Models;
    using PartyCLI.Data.Repositories;

    using RestSharp;

    [TestClass]
    public class ServerListCommandTests : TestBase
    {
        [TestMethod]
        public void DisplayServersFromStorage_DisplaysServerList()
        {
            using (var context = GetDbContext())
            {
                var servers = new List<Server>();

                for (var i = 0; i < 10; i++)
                {
                    servers.Add(new Server { Distance = i * 100, Name = $"TestServer{i}" });
                }

                context.Set<Server>().AddRange(servers);
                context.SaveChanges();

                var stringBuilder = new StringBuilder();
                var apiConfiguration = GetApiConfiguration();
                var apiProvider = new JsonApiProvider(new Mock<IRestClient>().Object, apiConfiguration);
                var command = new ServerListCommand(new ServerListOutputWriter(new StringBuilderOutputProvider(stringBuilder)), new EfGenericRepository(context), apiProvider, apiConfiguration);

                command.DisplayServersFromStorage();

                var expectedOutput = string.Join(
                    Environment.NewLine,
                    servers.Select(
                        s =>
                            $"{$"Server name: {s.Name}".PadLeft(ServerListConsoleOutputParams.ServerPaddingWidth, ' ')}, Distance: {s.Distance}")) + Environment.NewLine;

                Assert.AreEqual(expectedOutput, stringBuilder.ToString());
            }
        }

        [TestMethod]
        public void UpdateRecordsInStorage_DeletesOldServers_InsertsNewServers()
        {
            var servers = new List<Server>();
            var newServers = new List<Server>();

            for (var i = 0; i < 10; i++)
            {
                servers.Add(new Server { Id = i + 1, Distance = i * 100, Name = $"TestServer{i}" });
                newServers.Add(new Server { Distance = i * 100, Name = $"NewTestServer{i}" });
            }

            using (var context = GetDbContext())
            {
                context.Set<Server>().AddRange(servers);
                context.SaveChanges();

                var stringBuilder = new StringBuilder();
                var apiConfiguration = GetApiConfiguration();
                var apiProvider = new JsonApiProvider(new Mock<IRestClient>().Object, apiConfiguration);
                var command = new ServerListCommand(new ServerListOutputWriter(new StringBuilderOutputProvider(stringBuilder)), new EfGenericRepository(context), apiProvider, apiConfiguration);

                var serverIds = context.Set<Server>().Select(x => x.Id).ToList();
                command.UpdateRecordsInStorage(serverIds, newServers);

                Assert.AreEqual(newServers.Count, context.Set<Server>().Count());
                Assert.IsFalse(context.Set<Server>().ToList().Any(x => servers.Select(y => y.Id).Contains(x.Id)));

                foreach (var server in newServers)
                {
                    Assert.IsTrue(context.Set<Server>().ToList().Any(x => x.Name.Equals(server.Name) && x.Distance == server.Distance));
                }
            }
        }
    }
}
