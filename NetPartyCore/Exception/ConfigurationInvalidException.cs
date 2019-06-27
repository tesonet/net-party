namespace NetPartyCore.Exception
{
    class ConfigurationInvalidException : System.Exception
    {
        public ConfigurationInvalidException(): base("Invalid configuration parameters. Possible cause - mispelled or missing arguments")
        {
        }
    }
}
