using Microsoft.Extensions.Configuration;
using PartyCli.Core.Entities;
using PartyCli.Core.Interfaces;

namespace PartyCli.Infrastructure.Repositories
{
    public class ApiAuthCredencialsFileRepository : GenericFileRepository<ApiAuthCrediancials>, IApiAuthCredentialsRepository
    {
        public ApiAuthCredencialsFileRepository(ILogger logger, IConfiguration configuration) : base(logger, configuration)
        {
        }
    }
}
