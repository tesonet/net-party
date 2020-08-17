using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NetParty.Data.Http;
using Newtonsoft.Json;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace NetParty.Tests.Data.Http
{
    [TestFixture]
    internal class HttpClientExtensionTest : IDisposable
    {
        class  ResponseWrapper
        {
            public string Value { set; get; }
        }

        private MockHttpMessageHandler _server;
        private HttpClient _httpClient;

        private readonly string _url = "http://localhost/test";

        [SetUp]
        public void SetUp()
        {
            _server = new MockHttpMessageHandler
            {
                AutoFlush = true
            };

            _httpClient = _server.ToHttpClient();

        }

        [Test]
        public async Task PostAsync_SetsCorrectContentType()
        {
            // arrange
            _server.When(HttpMethod.Post,_url)
                .With(request => request.Content.Headers.ContentType.MediaType == "application/json")
                .Respond("application/json", JsonConvert.SerializeObject(new ResponseWrapper{Value = "Correct"}));
            
            // act
            var result = await _httpClient.PostAsync<ResponseWrapper>(new object(), _url);

            // verify
            Assert.AreEqual("Correct", result.Value);

        }

        [Test]
        public void PostAsync_ShouldThrowException_IfResponseStatusIsNotSuccess()
        {
            // arrange
            _server.When(HttpMethod.Post, _url)
                .Respond(HttpStatusCode.InternalServerError);

            // act and verify
            Assert.ThrowsAsync<Exception>(async () => await _httpClient.PostAsync<ResponseWrapper>(new object(), _url));
        }

        [Test]
        public void GetAsync_ShouldThrowException_IfResponseStatusIsNotSuccess()
        {
            // arrange
            _server.When(_url)
                .Respond(HttpStatusCode.InternalServerError);

            // act and verify
            Assert.ThrowsAsync<Exception>(async () => await _httpClient.GetAsync<ResponseWrapper>(_url, new Dictionary<string, string>()));
        }

        [Test]
        public async Task GetAsync_ShouldAppendHeaders_IfAnyGiven()
        {
            // arrange
            _server.When(_url)
                .WithHeaders("TestHeader", "TestValue")
                .Respond("application/json", JsonConvert.SerializeObject(new ResponseWrapper { Value = "Correct" }));

            _server.When(_url)
                .Respond("application/json", JsonConvert.SerializeObject(new ResponseWrapper { Value = "Inorrect" }));


            // act
            var result = await _httpClient.GetAsync<ResponseWrapper>(_url,
                new Dictionary<string, string>() {{"TestHeader", "TestValue"}});

            // verify
            Assert.AreEqual("Correct", result.Value);
        }

        public void Dispose()
        {
            _server?.Dispose();
            _httpClient?.Dispose();
        }
    }
}
