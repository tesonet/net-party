namespace PartyCLI.Tests.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using PartyCLI.Helpers;

    using RestSharp;

    [TestClass]
    public class JsonApiProviderTests : TestBase
    {
        [TestMethod]
        public void CreateRequest_CreatesValidRequest()
        {
            var parameters = new Dictionary<string, string>();
            var headers = new Dictionary<string, string>();

            for (var i = 0; i < 10; i++)
            {
                parameters.Add($"Parameter{i}", $"ParameterValue{i}");
                headers.Add($"Header{i}", $"HeaderValue{i}");
            }

            var request = RestRequestHelper.CreateRequest("testresource", parameters, headers);

            Assert.AreEqual("testresource", request.Resource);

            foreach (var param in parameters)
            {
                Assert.IsTrue(request.Parameters.Any(x => x.Name.Equals(param.Key) && x.Value.Equals(param.Value) && x.Type == ParameterType.GetOrPost));
            }

            foreach (var header in headers)
            {
                Assert.IsTrue(request.Parameters.Any(x => x.Name.Equals(header.Key) && x.Value.Equals(header.Value) && x.Type == ParameterType.HttpHeader));
            }
        }
    }
}