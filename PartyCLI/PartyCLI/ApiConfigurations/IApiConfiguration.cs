namespace PartyCLI.ApiConfigurations
{
    public interface IApiConfiguration
    {
        string Url { get; }

        string Username { get; }

        string Password { get; }
    }
}