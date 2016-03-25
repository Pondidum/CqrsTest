using Ledger;
using StructureMap;
using StructureMap.Graph;

namespace Tests
{
	public class TestRegistry : Registry
	{
		public TestRegistry(IEventStore store)
		{
			Scan(a =>
			{
				a.AssembliesFromApplicationBaseDirectory();
				a.LookForRegistries();
			});

			For<IEventStore>().Use(store);
		}
	}
}