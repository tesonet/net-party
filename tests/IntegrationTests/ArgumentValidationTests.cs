namespace TesonetDotNetParty.IntegrationTests
{
    using System.Threading.Tasks;
    using Xunit;

    [Collection("integration")]
    public class ArgumentValidationTests
    {
        [Theory]
        [InlineData("test")]
        [InlineData("definitely not")]
        [InlineData("bla bla bla")]
        public async Task ShouldNotExecuteOnInvalidSubCommand(string args)
        {
            const string ExpectedMessage = "Specify --help for a list of available options and commands.\r\n";

            await using var application = new CommandLineApplication(args.Split(' '));
            var returnCode = await application.RunAsync();

            Assert.Equal(ExpectedMessage, application.StandardOutput);
            Assert.Equal(255, returnCode);
        }

        [Theory]
        [InlineData("config", "The --username field is required.\r\n")]
        [InlineData("config --password password", "The --username field is required.\r\n")]
        [InlineData("config --username --password", "The --password field is required.\r\n")]
        public async Task ShouldNotExecuteConfigSubCommandWithoutRequiredOptions(string args, string errorMessage)
        {
            await using var application = new CommandLineApplication(args.Split(' '));
            var returnCode = await application.RunAsync();

            Assert.Equal(errorMessage, application.StandardError);
            Assert.Equal(1, returnCode);
        }

        [Theory]
        [InlineData("server_list asd")]
        [InlineData("server_list --option")]
        [InlineData("server_list bla bla bla")]
        public async Task ShouldNotExecuteServerListSubCommandWithInvalidOption(string args)
        {
            const string ExpectedMessage = "Specify --help for a list of available options and commands.\r\n";

            await using var application = new CommandLineApplication(args.Split(' '));
            var returnCode = await application.RunAsync();

            Assert.Equal(ExpectedMessage, application.StandardOutput);
            Assert.Equal(255, returnCode);
        }
    }
}
