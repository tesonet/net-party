using Newtonsoft.Json;
using partycli.Commands;
using partycli.core.Repositories.Model;
using partycli.core.Repositories.Storage;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace partycli.Tests.IntegrationTests
{
    [Collection("Integration")]
    public class ConfigCommandHandlerTests
    {
        [Theory]
        [InlineData("tesonet", "partyanimal")]
        public async Task ConfigCommandHandler_HandlesDefault(string username, string password)
        {
            ConfigCommand command = new ConfigCommand() { Username = username, Password = password };
            
            string credentialsPath = JsonConvert.DeserializeObject<StorageSettings>(new StreamReader("./StorageSettings.json").ReadToEnd()).CredentialsSavePath;

            if (File.Exists(credentialsPath))
                File.Delete(credentialsPath);

            await AbstractCommandHandler.StartWork(command, ContainerConfig.Configure());

            var credentials = JsonConvert.DeserializeObject<Credentials>(new StreamReader(credentialsPath).ReadToEnd());

            Assert.Equal(command.Username, credentials.Username);
            Assert.Equal(command.Password, credentials.Password);
        }
    }
}
