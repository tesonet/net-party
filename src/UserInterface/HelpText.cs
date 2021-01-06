namespace Tesonet.ServerListApp.UserInterface
{
    public static class HelpText
    {
        public static class Command
        {
            public const string Config = "Set API credentials";
            public const string ServerList = "Print a list of servers";
        }

        public static class Option
        {
            public const string Username = "Username for API authorization";
            public const string Password = "Password for API authorization";
            public const string Local = "Load data from persistent storage";
        }
    }
}