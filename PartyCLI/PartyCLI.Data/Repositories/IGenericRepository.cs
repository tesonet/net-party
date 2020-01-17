namespace PartyCLI.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using PartyCLI.Data.Models;

    public interface IGenericRepository
    {
        IQueryable<T> GetAll<T>() where T : BaseEntity;

        T GetById<T>(int id) where T : BaseEntity;

        void Add<T>(T entity) where T : BaseEntity;

        void AddRange<T>(IEnumerable<T> entities) where T : BaseEntity;

        void Update<T>(T entity) where T : BaseEntity;

        void Delete<T>(int id) where T : BaseEntity;

        DbContextTransaction GetTransactionScope();
    }
}