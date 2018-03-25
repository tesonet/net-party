using PartyCli.Models;

namespace PartyCli.Interfaces
{
    public interface ICredentialsRepository
    {
        Credentials Get();

        void Update(Credentials model);
    }
}
