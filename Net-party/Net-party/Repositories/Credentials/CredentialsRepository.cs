using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Net_party.Database;
using Net_party.Entities;
using Ninject;

namespace Net_party.Repositories
{
    class CredentialsRepository
    {
        private IStorage _storage;

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

        public Task<UserCredentials> GetLast()
        {
            var context = _storage.GetContext();
            try
            {
                var user = context.GetTable<UserCredentials>().AsEnumerable().LastOrDefault();
                return Task.FromResult(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
           
        }
    }
}
