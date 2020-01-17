namespace PartyCLI.Tests.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using ManyConsole;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PartyCLI.ConsoleCommands;
    using PartyCLI.ConsoleOutputWriters;
    using PartyCLI.Data.Models;
    using PartyCLI.Data.Repositories;

    [TestClass]
    public class ConfigCredentialsCommandTests : TestBase
    {
        private readonly string username = "testUsername";
        private readonly string password = "testPassword";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Command_Fails_OnMissingParameters()
        {
            using (var context = GetDbContext())
            {
                var command = new ConfigCredentialsCommand(new EfGenericRepository(context), new StringBuilderOutputProvider(new StringBuilder()));
                ConsoleCommandDispatcher.DispatchCommand(command, null, new StringWriter());
            }
        }

        [TestMethod]
        public void Command_Fails_OnMissingUsername()
        {
            using (var context = GetDbContext())
            {
                var command = new ConfigCredentialsCommand(new EfGenericRepository(context), new StringBuilderOutputProvider(new StringBuilder()));
                var result = ConsoleCommandDispatcher.DispatchCommand(command, new[] { "/password", password }, new StringWriter());

                Assert.AreEqual(FailedCommandResult, result);
                Assert.IsFalse(context.Set<ApiCredentials>().Any());
            }
        }

        [TestMethod]
        public void Command_Fails_OnMissingPassword()
        {
            using (var context = GetDbContext())
            {
                var command = new ConfigCredentialsCommand(new EfGenericRepository(context), new StringBuilderOutputProvider(new StringBuilder()));
                var result = ConsoleCommandDispatcher.DispatchCommand(command, new[] { "/username", username }, new StringWriter());

                Assert.AreEqual(FailedCommandResult, result);
                Assert.IsFalse(context.Set<ApiCredentials>().Any());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Command_Fails_OnEmptyUsername()
        {
            using (var context = GetDbContext())
            {
                var command = new ConfigCredentialsCommand(new EfGenericRepository(context), new StringBuilderOutputProvider(new StringBuilder()));
                ConsoleCommandDispatcher.DispatchCommand(command, new[] { "/username", string.Empty, "/password", password }, new StringWriter());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Command_Fails_OnEmptyPassword()
        {
            using (var context = GetDbContext())
            {
                var command = new ConfigCredentialsCommand(new EfGenericRepository(context), new StringBuilderOutputProvider(new StringBuilder()));
                ConsoleCommandDispatcher.DispatchCommand(command, new[] { "/username", username, "/password", string.Empty }, new StringWriter());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Command_Fails_OnExistingUsername()
        {
            using (var context = GetDbContext())
            {
                context.Set<ApiCredentials>().Add(new ApiCredentials { Username = username, Password = password});
                context.SaveChanges();

                var command = new ConfigCredentialsCommand(new EfGenericRepository(context), new StringBuilderOutputProvider(new StringBuilder()));
                ConsoleCommandDispatcher.DispatchCommand(command, new[] { "/username", username, "/password", string.Empty }, new StringWriter());
            }
        }

        [TestMethod]
        public void Command_Succeeds_OnValidParameters()
        {
            using (var context = GetDbContext())
            {
                var provider = new SHA512CryptoServiceProvider();
                var encoding = new UnicodeEncoding();
                var hash = provider.ComputeHash(encoding.GetBytes(password));
                var encodedPassword = Encoding.UTF8.GetString(hash);

                var command = new ConfigCredentialsCommand(new EfGenericRepository(context), new StringBuilderOutputProvider(new StringBuilder()));
                var consoleOutput = new StringBuilder();
                var result = ConsoleCommandDispatcher.DispatchCommand(command, new[] { "/username", username, "/password", password }, new StringWriter(consoleOutput));

                Assert.AreEqual(SuccessfulCommandResult, result);
                Assert.AreEqual(1, context.Set<ApiCredentials>().Count());
                Assert.IsTrue(context.Set<ApiCredentials>().Any(x => x.Username.Equals(username) && x.Password.Equals(encodedPassword)));
            }
        }
    }
}
