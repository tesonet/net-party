using Tesonet.NetParty.Models.Interfaces;
using Tesonet.NetParty.Services;
using Tesonet.NetParty.TextFileRepository;
using Unity;

namespace Tesonet.NetParty
{
	public class UnityContainerManager
	{
		private readonly UnityContainer _container;

		public UnityContainerManager()
		{
			_container = new UnityContainer();
			_container.RegisterType<IDataProvider, PlayGroundService>();
			_container.RegisterType<IDataRepository, FileRepository>();
		}

		public IDataProvider GetDataProvider(string name = null)
		{
			return _container.Resolve<IDataProvider>(name);
		}

		public IDataRepository GetDataRepository(string name = null)
		{
			return _container.Resolve<IDataRepository>(name);
		}
	}
}
