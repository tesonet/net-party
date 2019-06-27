namespace NetPartyCore.Exception
{
    class ConfigurationNotFoundException : System.Exception
    {
        public ConfigurationNotFoundException(): base("Configuration not found the datastore. Possible cause - configuration was not saved.")
        {
        }
    }
}
