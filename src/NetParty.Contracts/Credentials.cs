using System;

namespace NetParty.Contracts
{
    [Serializable]
    public class Credentials
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
