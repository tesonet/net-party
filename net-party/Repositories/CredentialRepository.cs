﻿using net_party.Entities.Database;
using net_party.Repositories.Contracts;
using System;
using System.Threading.Tasks;

namespace net_party.Repositories
{
    public class CredentialRepository : BaseRepository<Credential>, ICredentialRepository
    {
        public CredentialRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<Credential> Get()
        {
            var sql = $"SELECT TOP 1 * FROM {nameof(Credential)}s";
            return await FirstOrNullAsync(sql);
        }
    }
}