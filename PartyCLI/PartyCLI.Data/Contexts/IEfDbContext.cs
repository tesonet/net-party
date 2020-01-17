namespace PartyCLI.Data.Contexts
{
    using System;
    using System.Data.Entity;

    using PartyCLI.Data.Models;

    public interface IEfDbContext : IDisposable
    {
        DbSet<Server> Servers { get; set; }

        DbSet<ApiCredentials> ApiCredentials { get; set; }

        int SaveChanges();
    }
}