namespace Tesonet.ServerListApp.Infrastructure.Http
{
    using System.Text.Json;

    public static class JsonOptions
    {
        public static readonly JsonSerializerOptions Default = new(JsonSerializerDefaults.Web);
    }
}
