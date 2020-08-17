using System;
using System.IO;
using NUnit.Framework;
using SlavaGu.ConsoleAppLauncher;

namespace NetParty.IntegrationTests
{
    [TestFixture]
    class FullWorkflowTest
    {
        private string path;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var dir = Path.GetDirectoryName(typeof(FullWorkflowTest).Assembly.Location);
            if (dir != null)
            {
                Environment.CurrentDirectory = dir;
                Directory.SetCurrentDirectory(dir);

                path = $"{dir}\\partycli.exe";
            }
            else
                throw new Exception("Path.GetDirectoryName(typeof(TestingWithReferencedFiles).Assembly.Location) returned null");
        }

        [TearDown]
        public void CleanUpState()
        {
            File.Delete($"{Environment.CurrentDirectory}\\crd");
            File.Delete($"{Environment.CurrentDirectory}\\srv");
        }


        [Test]
        public void FreshState()
        {
            // act
            var localServerList = ConsoleApp.Run(path, "server_list --local").Output;
            var remoteServerList = ConsoleApp.Run(path, "server_list").Output;
            var credentialsSave = ConsoleApp.Run(path, "config --username tesonet --password partyanimal").Output;

            // verify
            StringAssert.Contains("Total: 0", localServerList);

            StringAssert.Contains("Couldn't refresh servers due to an exception: Application credentials must be configured first.", remoteServerList);

            StringAssert.Contains("Updating credentials", credentialsSave);
            StringAssert.Contains("Done", credentialsSave);
        }

        [Test]
        public void CredentialsConfigured()
        {
            // arrange
            ConsoleApp.Run(path, "config --username tesonet --password partyanimal");

            // act
            var localServerList = ConsoleApp.Run(path, "server_list --local").Output;
            var remoteServerList = ConsoleApp.Run(path, "server_list").Output;
            var afterReloadingLocalServerList = ConsoleApp.Run(path, "server_list --local").Output;

            // verify
            StringAssert.Contains("Total: 0", localServerList);

            StringAssert.Contains("Listing servers:", remoteServerList);
            StringAssert.Contains("Latvia", remoteServerList);
            StringAssert.Contains("United States", remoteServerList);
            StringAssert.Contains("Total: 30", remoteServerList);

            Assert.AreEqual(remoteServerList, afterReloadingLocalServerList);
        }

    }
}