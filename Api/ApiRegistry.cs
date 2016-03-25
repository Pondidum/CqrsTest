using Domain.Infrastructure;
using Ledger;
using StructureMap;
using StructureMap.Graph;

namespace Api
{
	public class ApiRegistry : Registry
	{
		public ApiRegistry(IEventStore store)
		{
			Scan(a =>
			{
				a.TheCallingAssembly();
				a.AssembliesFromApplicationBaseDirectory();
				a.LookForRegistries();
			});
			
			For<IEventStore>()
				.Use(context => new ProjectionStore(store, context.GetInstance<Projectionist>().Apply));
		}
	}
}