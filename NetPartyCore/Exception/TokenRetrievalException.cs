namespace NetPartyCore.Exception
{
    class TokenRetrievalException : System.Exception
    {
        public TokenRetrievalException(): base("Failed to retrive api token. Possible cause - invalid configuration.")
        {
        }
    }
}
