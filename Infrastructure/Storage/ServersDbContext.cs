#nullable disable
namespace Tesonet.ServerListApp.Infrastructure.Storage
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using ServerListApi;

    public class ServersDbContext : DbContext
    {
        private readonly ILogger<ServersDbContext> _logger;

        [UsedImplicitly]
        public DbSet<ClientCredentials> ClientCredentials { get; set; }

        [UsedImplicitly]
        public DbSet<Server> Servers { get; set; }

        public ServersDbContext()
        {
            _logger = NullLogger<ServersDbContext>.Instance;
        }

        public ServersDbContext(ILogger<ServersDbContext> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Checks if any pending migrations exist and applies them, if necessary.
        /// </summary>
        /// <returns></returns>
        public static async Task MigrateDatabase()
        {
            await using var dbContext = new ServersDbContext();

            var pendingMigrations = (await dbContext.Database.GetPendingMigrationsAsync()).ToList();

            if (pendingMigrations.Any())
            {
                Debug.WriteLine($"Pending migrations: {string.Join(',', pendingMigrations)}");
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.MigrateAsync();
            }
        }

        /// <summary>
        /// Executes TRUNCATE against the provided database table.
        /// Use with extreme caution.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task Truncate<TEntity>() where TEntity : class
        {
            var tableName = Set<TEntity>().EntityType.GetTableName();
            await Database.ExecuteSqlRawAsync($"DELETE FROM {tableName}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Data Source=servers.db")
                .LogTo(message => _logger.LogInformation(message), LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientCredentials>().ToTable("ClientCredentials");
            modelBuilder.Entity<ClientCredentials>(builder =>
            {
                builder.HasKey(e => e.Username);
                builder.Property(e => e.Username).IsRequired();
                builder.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Server>().ToTable("Servers");
            modelBuilder.Entity<Server>(builder =>
            {
                builder.HasKey(s => s.Id);
                builder.Property(s => s.Id).IsRequired();
                builder.Property(s => s.Name).IsRequired();
                builder.Property(s => s.Distance).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}