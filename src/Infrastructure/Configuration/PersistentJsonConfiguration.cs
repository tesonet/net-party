namespace Tesonet.ServerListApp.Infrastructure.Configuration
{
    using System;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    internal class PersistentJsonConfiguration : IPersistentConfiguration
    {
        private readonly ILogger<PersistentJsonConfiguration> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _filePath;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = true
        };

        public PersistentJsonConfiguration(ILogger<PersistentJsonConfiguration>? logger = null)
        {
            _logger = logger ?? NullLogger<PersistentJsonConfiguration>.Instance;
            _filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

            _configuration = new ConfigurationBuilder()
                .AddJsonFile(_filePath, true)
                .Build();
        }

        public T GetSection<T>(string section) where T : class, new()
        {
            var obj = new T();
            _configuration.GetSection(section).Bind(obj);
            return obj;
        }

        public async Task Save<T>(T obj) where T : class
        {
            var json = JsonSerializer.Serialize(obj, _jsonSerializerOptions);
            await File.WriteAllTextAsync(_filePath, json);

            _logger.LogInformation($"Configuration saved to {_filePath}.");
        }
    }
}