namespace NetParty.CLI.Options
{
    public class Constants
    {
        public class Verbs
        {
            public class Config
            {
                public const string Name = "config";
                public const string HelpText = "Manage configuration options for .NET Party CLI";

                public class Options
                {
                    public class Username
                    {
                        public const string Name = "username";
                        public const string HelpText = "Username used when autorizating with the server";
                    }
                    public class Password
                    {
                        public const string Name = "password";
                        public const string HelpText = "Password used when autorizating with the server";
                    }
                }
            }

            public class ServerList
            {
                public const string Name = "server_list";
                public const string HelpText = "List available servers";
                public class Options
                {
                    public class Local
                    {
                        public const string Name = "local";
                        public const string HelpText = "Return localy persisted servers";
                    }
                }
            }
        }
    }
}
