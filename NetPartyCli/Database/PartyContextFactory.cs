using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NetPartyCli.Database;

namespace NetPartyCli.Database
{
    public class PartyContextFactory : IDesignTimeDbContextFactory<PartyContext>
    {
        public PartyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PartyContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PartyDB");
            return new PartyContext(optionsBuilder.Options);
        }
    }
}