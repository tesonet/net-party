using Microsoft.EntityFrameworkCore;
using partycli.Contracts.Entities;

namespace partycli.DataAccess
{
    public class PartyContext: DbContext
    {
        public PartyContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<ConfigurationEntity> Configuration { get; set; }
        public DbSet<ServerEntity> Servers { get; set; }
    }
}
