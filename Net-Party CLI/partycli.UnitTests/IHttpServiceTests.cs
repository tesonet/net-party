using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Flurl;
using Flurl.Http.Testing;
using partycli.Http;
using System.Net.Http;
using partycli.Helpers;
using System.Dynamic;
using FluentAssertions;

namespace partycli.UnitTests.IHttpServiceTests
{
    [TestFixture]
    class IHttpServiceTests
    {
        [Test]
        public async Task HttpService_GetWithToken_MakesHttpGetRequrest()
        {
            //Arrange
            var httpService = new HttpService("https://api.com/");
            string server_list = "[{\"name\":\"United States #3\",\"distance\":627},{\"name\":\"Japan #78\",\"distance\":1107}]";
            using (var httpTest = new HttpTest())
            {
                // arrange
                httpTest.RespondWith(server_list);
                // act
                var result = await httpService.GetWithToken("123");
                // assert
                httpTest.ShouldHaveCalled("https://api.com/")
                    .WithVerb(HttpMethod.Get)
                    .WithHeader("Accept", "application/json")
                    .WithHeader("Authorization", "Bearer 123");                    
            }
        }

        [Test]
        public async Task HttpService_GetWithToken_ReturnsSerializedList()
        {
            //Arrange     
            var httpService = new HttpService("https://api.com/");
            string server_list = "[{\"name\":\"United States #3\",\"distance\":627},{\"name\":\"Japan #78\",\"distance\":1107}]";
            using (var httpTest = new HttpTest())
            {
                // arrange
                httpTest.RespondWith(server_list, 200);
                // act
                var result = await httpService.GetWithToken("123");
                // assert
                result.Result.Should().BeEquivalentTo(server_list);
            }
        }

        [Test]
        public async Task HttpService_PostJson_MakesHttpPostRequest()
        {
            //Arrange     
            var httpService = new HttpService("https://api.com/");
            dynamic response = new ExpandoObject();
            response.token = "123";
            using (var httpTest = new HttpTest())
            {
  
                httpTest.RespondWithJson(response);
                // act
                var result = await httpService.PostJson("serializedCredentials");
                // assert
                httpTest.ShouldHaveCalled("https://api.com/")
                    .WithVerb(HttpMethod.Post)
                    .WithHeader("Content-Type", "application/json");
            }
        }

        [Test]
        public async Task HttpService_PostJson_ReturnsSuccessResult()
        {
            //Arrange     
            var httpService = new HttpService("https://api.com/");
            dynamic response = new ExpandoObject();
            response.token = "123";
            using (var httpTest = new HttpTest())
            {

                httpTest.RespondWithJson(response);
                // act
                var result = await httpService.PostJson("serializedCredentials");
                // assert
                result.Should().BeOfType<SuccessResult<string>>();
                result.Result.Should().Be("123");
            }
        }

        [Test]
        public async Task HttpService_PostJsonWithIncorrectCredentials_eturnsFailedResult()
        {
            //Arrange
            var httpService = new HttpService("https://api.com/");
            dynamic response = new ExpandoObject();
            response.message = "unauthorized";
            using (var httpTest = new HttpTest())
            {

                httpTest.RespondWithJson(response);
                // act
                var result = await httpService.PostJson("serializedCredentials");
                // assert
                result.Should().BeOfType<FailedResult>();
                result.ErrorMessage.Should().Be("unauthorized");
            }
        }
    }
}
