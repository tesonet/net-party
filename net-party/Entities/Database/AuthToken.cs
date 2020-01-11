using net_party.Entities.API;
using System;

namespace net_party.Entities.Database
{
    public class AuthToken
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }

        public string Token { get; set; }

        public AuthToken()
        {
        }

        public static AuthToken FromTokenResponse(TokenResponse response)
        {
            return new AuthToken()
            {
                Token = response.Token
            };
        }
    }
}