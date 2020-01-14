using Dapper.Contrib.Extensions;
using net_party.Entities.API;
using net_party.Entities.Interfaces;
using System;

namespace net_party.Entities.Database
{
    public class AuthToken : IEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime AddedDate { get; set; }
        public string Token { get; set; }

        public AuthToken()
        {
        }

        public static AuthToken FromTokenResponse(TokenResponse response)
        {
            return new AuthToken()
            {
                AddedDate = DateTime.UtcNow,
                Token = response.Token
            };
        }
    }
}