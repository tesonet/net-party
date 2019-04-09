using Newtonsoft.Json;
using partycli.Commands;
using partycli.core.Repositories.Model;
using partycli.core.Repositories.Storage;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace partycli.Tests.IntegrationTests
{
    [Collection("Integration")]
    public class ServerListCommandHandlerTestsNotLocal
    {
        [Fact]
        public async Task ServerListCommandHandler_HandlesNotLocal()
        {
            ServerListCommand command = new ServerListCommand() { FetchLocal = false };

            string serversPath = JsonConvert.DeserializeObject<StorageSettings>(new StreamReader("./StorageSettings.json").ReadToEnd()).ServerSavePath;

            if (File.Exists(serversPath))
                File.Delete(serversPath);

            await AbstractCommandHandler.StartWork(command, ContainerConfig.Configure());

            IEnumerable<Server> servers = null;
            using (var sr = new StreamReader(serversPath))
            {
                servers = JsonConvert.DeserializeObject<IEnumerable<Server>>(sr.ReadToEnd());
            }

            Assert.NotNull(servers);
            Assert.NotEmpty(servers);
        }
    }
}
