namespace Tesonet.ServerListApp.Infrastructure.ServerListApi
{
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ServersListApiConfig
    {
        public string Username { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public string BaseAddress { get; init; } = string.Empty;

        public Credentials ClientCredentials => new(Username, Password);

        public record Credentials(string Username, string Password);
    }
}