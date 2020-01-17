namespace PartyCLI.ApiConfigurations
{
    using System.Configuration;

    public class TesonetPlaygroundApiConfiguration : ConfigurationSection, IApiConfiguration
    {
        [ConfigurationProperty("Url", IsRequired = true)]
        public string Url => this["Url"].ToString();

        [ConfigurationProperty("Username", IsRequired = true)]
        public string Username => this["Username"].ToString();

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password => this["Password"].ToString();
    }
}