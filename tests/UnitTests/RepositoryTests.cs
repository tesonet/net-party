namespace TesonetDotNetParty.UnitTests
{
    using Tesonet.ServerListApp.Infrastructure.Storage;
    using Xunit;

    public class RepositoryTests
    {
        public RepositoryTests()
        {
            var r = new ServersRepository(new ServersDbContext());
        }

        [Fact]
        public void Test1()
        {
        }
    }
}