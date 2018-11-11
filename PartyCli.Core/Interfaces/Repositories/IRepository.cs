using System;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
    }
}
