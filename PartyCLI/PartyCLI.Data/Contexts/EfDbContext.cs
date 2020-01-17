namespace PartyCLI.Data.Contexts
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;

    using PartyCLI.Data.Models;

    public class EfDbContext : DbContext, IEfDbContext
    {
        public EfDbContext() : base("PartyCLIConnectionString")
        {
        }

        public EfDbContext(DbConnection connection, bool contextOwnConnection) : base(connection, contextOwnConnection)
        {
        }

        public DbSet<Server> Servers { get; set; }

        public DbSet<ApiCredentials> ApiCredentials { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => $"{x.PropertyName} : {x.ErrorMessage}");

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");

            var serverEntity = modelBuilder.Entity<Server>();

            serverEntity.ToTable("Servers");
            serverEntity.HasKey(x => x.Id);
            serverEntity.Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            serverEntity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            serverEntity.Property(x => x.Distance).IsRequired();

            var apiCredentialsEntity = modelBuilder.Entity<ApiCredentials>();

            apiCredentialsEntity.ToTable("ApiCredentials");
            apiCredentialsEntity.HasKey(x => x.Id);
            apiCredentialsEntity.Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            apiCredentialsEntity.Property(x => x.Username).IsRequired().HasMaxLength(100);
            apiCredentialsEntity.Property(x => x.Password).IsRequired().HasMaxLength(100);
        }
    }
}