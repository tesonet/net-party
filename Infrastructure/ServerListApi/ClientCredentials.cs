namespace Tesonet.ServerListApp.Infrastructure.ServerListApi
{
    public record ClientCredentials(string Username, string Password)
    {
        public static ClientCredentials Empty => new(string.Empty, string.Empty);
    }
}