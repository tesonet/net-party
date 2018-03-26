using System.Collections.Generic;

namespace PartyCli.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        void Update(IEnumerable<T> items);
    }
}
