using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetPartyCore.Framework;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace NetPartyTest.Framework
{
    [TestClass]
    public class CommandRouterTest
    {
        private CommandRouter router;

        [TestInitialize]
        public void SetupTestCase()
        {
            router = new CommandRouter();
        }

        [TestMethod]
        public void SetupTest()
        {
            var description = "Tesonet .NET developer job interview code writing test.";

            Assert.AreEqual(description, router.GetRootCommand().Description);
        }

        [TestMethod]
        public async Task AddRouteTest()
        {
            string[] args = { "test-command", "--test", "true" };

            router.AddRoute("test-command", "test", new List<Option>() {
                new Option("--test", "Api client username", new Argument<bool>())
            }, CommandHandler.Create<bool>((value) => {
                return 10;
            }));

            var result = await router.GetRootCommand()
                .InvokeAsync(args);

            Assert.AreEqual(result, 10);
        }
    }

    internal interface ICallCheck
    {
        void CallCheck(bool value);
    }
}
