using net_party.Entities.Database;
using net_party.Repositories.Contracts;
using System;
using System.Data;
using System.Threading.Tasks;

namespace net_party.Repositories
{
    public class AuthTokenRepository : BaseRepository<AuthToken>, IAuthTokenRepository
    {
        public AuthTokenRepository(IServiceProvider services) : base(services)
        {
        }

        public async Task<AuthToken> Get()
        {
            if (_connection.State == ConnectionState.Broken || _connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            var sql = $"SELECT TOP 1 * FROM {nameof(AuthToken)}s";
            return await FirstOrNullAsync(sql);
        }
    }
}