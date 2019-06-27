namespace NetPartyCore.Exception
{
    class ServerListRetrievalException : System.Exception
    {
        public ServerListRetrievalException(): base("Failed to retrive remote server list. Possible cause - network communication.")
        {
        }
    }
}
