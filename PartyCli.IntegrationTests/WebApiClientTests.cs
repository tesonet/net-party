using Castle.Core.Logging;
using Moq;
using NUnit.Framework;
using PartyCli.WebApiClient.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PartyCli.IntegrationTests
{
  [TestFixture]
  public class WebApiClientTests
  {
    private Mock<ILogger> _loggerMock;
    private Mock<IWebApiClientSettings> _settingsMock;

    [SetUp]
    public void Setup()
    {
      _loggerMock = new Mock<ILogger>();
      _settingsMock = new Mock<IWebApiClientSettings>();

      _settingsMock.Setup(x => x.WebApiBaseUrl).Returns("http://playground.tesonet.lt");
    }

    [Test]
    public async Task WebApi_Should_Return_AccessToken()
    {
      var webApi = new WebApiClient.WebApiClient(_settingsMock.Object, _loggerMock.Object);
      var token = await webApi.GetTokenAsync("tesonet", "partyanimal");
      
      Assert.IsNotEmpty(token.Token);   
    }

    [Test]
    public async Task WebApi_Should_Return_AccessToken_And_Servers()
    {
      var webApi = new WebApiClient.WebApiClient(_settingsMock.Object, 
        _loggerMock.Object);

      var token = await webApi.GetTokenAsync("tesonet", "partyanimal");
      Assert.IsNotEmpty(token.Token);

      var servers = await webApi.GetServersAsync(token.Token);            
      Assert.IsTrue(servers.Any());
    }
  }
}
