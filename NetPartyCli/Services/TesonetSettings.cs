namespace NetPartyCli.Services
{
    public class TesonetSettings
    {
        public string TokenResource { get; internal set; } = "http://playground.tesonet.lt/v1/tokens";
        public string ServerResource { get; internal set; } = "http://playground.tesonet.lt/v1/servers";
    }
}