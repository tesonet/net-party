using NetParty.Model.Entities;

namespace NetParty.Model.Services
{
    public interface IApiService
    {
        GenerateTokenResponse GenerateToken(Credentials args);
        GetServersResponse GetServersList(GetApiServersArgs args);
    }
}