namespace partycli.core.DataAccess
{
    public class ApiSettings
    {
        public ApiSettings()
        {
            ServerUri = "http://playground.tesonet.lt/v1/servers";
            TokenUri = "http://playground.tesonet.lt/v1/tokens";
        }

        public string ServerUri { get; set; }
        public string TokenUri { get; set; }
    }
}
