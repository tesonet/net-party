using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Net_party.Database;
using Net_party.Entities;
using Net_party.Logging;
using Ninject;

namespace Net_party.Repositories.Credentials
{
    class CredentialsRepository : ICredentialsRepository
    {
        private readonly IStorage _storage;

        public CredentialsRepository()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _storage = kernel.Get<IStorage>();
        }

        public Task SaveUser(UserCredentials entity)
        {
            var context = _storage.GetContext();
            context.GetTable<UserCredentials>().InsertOnSubmit(entity);
            context.SubmitChanges();
            return Task.CompletedTask;
        }

        public async Task<UserCredentials> GetLast()
        {
            return await ExceptionLogging.CatchAndLogErrors(() =>
            {
                var context = _storage.GetContext();
                var user = context.GetTable<UserCredentials>().AsEnumerable().LastOrDefault();
                return Task.FromResult(user);
            });
        }
    }
}
