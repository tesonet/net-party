namespace TesonetDotNetParty.UnitTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Tesonet.ServerListApp.Domain;
    using Tesonet.ServerListApp.Infrastructure.Storage;
    using Xunit;

    public class RepositoryTests
    {
        private readonly ServersDbContext _dbContext;
        private readonly IServersRepository _repository;

        public RepositoryTests()
        {
            _dbContext = new ServersDbContext("Data Source=InMemory;Mode=Memory;Cache=Shared");
            _repository = new ServersRepository(_dbContext);
        }

        [Fact]
        public async Task ReturnsDataFromStorage()
        {
            var expectedServers = new[]
            {
                new Server(Guid.NewGuid(), "Lithuania #1", 20),
                new Server(Guid.NewGuid(), "Latvia #1", 500),
                new Server(Guid.NewGuid(), "Estonia #1", 750)
            };

            await _dbContext.MigrateDatabase();
            await _dbContext.Servers.AddRangeAsync(expectedServers);
            await _dbContext.SaveChangesAsync();

            var result = (await _repository.GetAll()).ToList();
            Assert.Equal(3, result.Count);

            foreach (var (id, name, distance) in expectedServers)
            {
                var server = result.Find(s => s.Id == id)!;

                Assert.NotNull(server);
                Assert.Equal(id, server.Id);
                Assert.Equal(name, server.Name);
                Assert.Equal(distance, server.Distance);
            }
        }

        [Fact]
        public async Task RemovesOldServersAndStoresNewServers()
        {
            await _dbContext.MigrateDatabase();

            await _dbContext.AddRangeAsync(
                new Server(Guid.NewGuid(), "Lithuania #1", 20),
                new Server(Guid.NewGuid(), "Latvia #1", 500));

            await _dbContext.SaveChangesAsync();

            var expectedServers = new[]
            {
                new Server(Guid.NewGuid(), "Latvia #1", 999),
                new Server(Guid.NewGuid(), "Germany #1", 1234)
            };

            await _repository.Store(expectedServers);

            var result = await _dbContext.Servers.ToListAsync();
            Assert.Equal(2, result.Count);

            foreach (var (id, name, distance) in expectedServers)
            {
                var server = result.Find(s => s.Id == id)!;

                Assert.NotNull(server);
                Assert.Equal(id, server.Id);
                Assert.Equal(name, server.Name);
                Assert.Equal(distance, server.Distance);
            }
        }
    }
}