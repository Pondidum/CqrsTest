using StructureMap;

namespace ReadModels
{
	public class ReadModelRegistry : Registry
	{
		public ReadModelRegistry()
		{
			For<AllUsers>().Singleton();
		}
	}
}