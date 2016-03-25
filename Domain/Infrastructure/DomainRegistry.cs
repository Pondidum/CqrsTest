using Domain.Services;
using Ledger;
using StructureMap;
using StructureMap.Graph;

namespace Domain.Infrastructure
{
	public class DomainRegistry : Registry
	{
		public DomainRegistry()
		{
			Scan(a =>
			{
				a.TheCallingAssembly();
				a.WithDefaultConventions();
			});

			For<Projectionist>().Singleton();

			For<UserService>().Singleton();
		}
	}
}
