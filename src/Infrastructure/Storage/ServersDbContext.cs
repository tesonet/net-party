#nullable disable
namespace Tesonet.ServerListApp.Infrastructure.Storage
{
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    public class ServersDbContext : DbContext
    {
        private readonly ILogger<ServersDbContext> _logger;

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
        public async Task MigrateDatabase()
        {
            var pendingMigrations = (await Database.GetPendingMigrationsAsync()).ToList();

            if (pendingMigrations.Any())
            {
                var message = $"Pending migrations: {string.Join(',', pendingMigrations)}";
                _logger.LogInformation(message);

                await Database.EnsureDeletedAsync();
                await Database.MigrateAsync();
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