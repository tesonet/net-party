using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPartyCore.Network;
using Refit;

namespace NetPartyTest.Network
{
    [TestClass]
    public class RemoteApiTest
    {
        [TestMethod]
        public void SetupTest()
        {
            // there should be no need to test api calls since they are taken care by Refit
            var api = RestService.For<IRemoteApi>("http://playground.tesonet.lt/v1/");
            Assert.IsNotNull(api);
        }
    }
}
