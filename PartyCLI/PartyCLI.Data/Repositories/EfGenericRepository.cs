namespace PartyCLI.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;

    using PartyCLI.Data.Models;

    public class EfGenericRepository : IGenericRepository
    {
        private readonly DbContext efDbContext;

        public EfGenericRepository(DbContext context)
        {
            efDbContext = context;
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return efDbContext.Set<T>();
        }

        public T GetById<T>(int id) where T : BaseEntity
        {
            return efDbContext.Set<T>().Find(id);
        }

        public void Add<T>(T entity) where T : BaseEntity
        {
            efDbContext.Set<T>().Add(entity);

            efDbContext.SaveChanges();
        }

        public void AddRange<T>(IEnumerable<T> entities)
            where T : BaseEntity
        {
            efDbContext.Set<T>().AddRange(entities);

            efDbContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            efDbContext.Set<T>().Attach(entity);
            efDbContext.Entry(entity).State = EntityState.Modified;

            efDbContext.SaveChanges();
        }

        public void Delete<T>(int id) where T : BaseEntity
        {
            var entity = efDbContext.Set<T>().Find(id);

            if (entity != null)
            {
                efDbContext.Set<T>().Remove(entity);
            }
        }

        public DbContextTransaction GetTransactionScope()
        {
            return efDbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }
    }
}