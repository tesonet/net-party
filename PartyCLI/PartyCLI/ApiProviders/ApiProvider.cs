namespace PartyCLI.ApiProviders
{
    using System.Collections.Generic;

    public abstract class ApiProvider
    {
        public abstract T Post<T>(string resource, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null) where T : new();

        public abstract T Get<T>(string resource, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null) where T : new();
    }
}