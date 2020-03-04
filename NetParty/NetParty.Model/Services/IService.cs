using NetParty.Model.Entities;

namespace NetParty.Model.Services
{
    public interface IService
    {
        SaveCredentialsResponse SaveCredentials(Credentials args);
        GetServersResponse GetServersList(GetServersArgs args);
    }
}