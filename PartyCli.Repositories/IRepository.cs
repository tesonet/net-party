using PartyCli.Entities;
using PartyCli.Entities.Interfaces;
using System.Collections.Generic;

namespace PartyCli.Repositories
{
  public interface IRepository<T> where T : IEntity
  {
    ICollection<T> FindAll();
    void InsertBulk(IEnumerable<T> entities);
    void Truncate();
  }    
}
