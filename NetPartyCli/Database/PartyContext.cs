using Microsoft.EntityFrameworkCore;
namespace NetPartyCli.Database
{
    public class PartyContext : DbContext
    {
        public PartyContext(DbContextOptions<PartyContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Server> Servers { get; set; }
    }
}
