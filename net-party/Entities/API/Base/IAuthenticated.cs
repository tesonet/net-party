namespace net_party.Entities.API.Base
{
    public interface IAuthenticated
    {
        string AuthToken { get; set; }
    }
}