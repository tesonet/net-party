namespace PartyCLI.Tests
{
    using System.Data.Entity;

    using Effort;

    using Moq;

    using PartyCLI.ApiConfigurations;
    using PartyCLI.Data.Contexts;

    public class TestBase
    {
        protected const int FailedCommandResult = -1;
        protected const int SuccessfulCommandResult = 0;

        protected DbContext GetDbContext()
        {
            var connection = DbConnectionFactory.CreateTransient();
            var context = new EfDbContext(connection, false);
            context.Database.CreateIfNotExists();

            return context;
        }

        protected IApiConfiguration GetApiConfiguration()
        {
            var apiConfiguration = new Mock<IApiConfiguration>();
            apiConfiguration.Setup(x => x.Username).Returns("testApiUsername");
            apiConfiguration.Setup(x => x.Password).Returns("testApiPassword");
            apiConfiguration.Setup(x => x.Url).Returns("https://test.restapi.com/v1");

            return apiConfiguration.Object;
        }
    }
}