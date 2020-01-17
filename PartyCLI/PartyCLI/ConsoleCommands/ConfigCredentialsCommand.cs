namespace PartyCLI.ConsoleCommands
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using ManyConsole;

    using PartyCLI.ConsoleOutputWriters;
    using PartyCLI.Data.Models;
    using PartyCLI.Data.Repositories;

    public class ConfigCredentialsCommand : ConsoleCommand
    {
        private readonly IGenericRepository repository;
        private readonly IOutputProvider outputProvider;
        private string username;
        private string password;

        public ConfigCredentialsCommand(IGenericRepository repository, IOutputProvider outputProvider)
        {
            IsCommand("config", "Configures API credentials.");
            HasRequiredOption("username=", "API user Username.", u => username = u);
            HasRequiredOption("password=", "API user Password.", p => password = p);

            this.repository = repository;
            this.outputProvider = outputProvider;
        }

        public override int Run(string[] remainingArguments)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            var userExists = repository.GetAll<ApiCredentials>().Any(x => x.Username.Equals(username));

            if (userExists)
            {
                throw new ArgumentException($"Supplied username '{username}' already exists in the persistent data store.");
            }

            var provider = new SHA512CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            var hash = provider.ComputeHash(encoding.GetBytes(password));
            var encodedPassword = Encoding.UTF8.GetString(hash);

            repository.Add(new ApiCredentials
            {
                Password = encodedPassword,
                Username = username
            });

            outputProvider.WriteLine($@"API credentials successfully saved. {Constants.ExitMessage}");

            if (Process.GetCurrentProcess().ProcessName != Constants.TestHostProcessName)
            {
                Console.ReadKey();
            }

            return 0;
        }
    }
}