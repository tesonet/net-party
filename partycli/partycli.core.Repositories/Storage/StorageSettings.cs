namespace partycli.core.Repositories.Storage
{
    public class StorageSettings
    {
        public StorageSettings()
        {
            ServerSavePath = "./Servers.json";
            CredentialsSavePath = "./Creds.json";
        }

        public string ServerSavePath { get; set; }
        public string CredentialsSavePath { get; set; }
    }
}
