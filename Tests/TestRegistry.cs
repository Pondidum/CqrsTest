using Domain.Infrastructure;
using Ledger;
using MediatR;
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
				a.TheCallingAssembly();
				a.AssembliesFromApplicationBaseDirectory();
				a.LookForRegistries();

				a.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
				a.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
				a.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
				a.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
			});

			For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
			For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));

			For<IMediator>().Use<Mediator>();

			For<IEventStore>()
				.Use(context => new ProjectionStore(store, context.GetInstance<Projectionist>().Apply));
		}
	}
}