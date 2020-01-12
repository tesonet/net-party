﻿using net_party.Entities.Database;
using System.Threading.Tasks;

namespace net_party.Repositories.Contracts
{
    public interface IAuthTokenRepository : IBaseRepository<AuthToken>
    {
        Task<AuthToken> Get();
    }
}