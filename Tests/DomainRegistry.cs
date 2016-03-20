using Domain;
using Ledger;
using MediatR;
using StructureMap;
using StructureMap.Graph;

namespace Tests
{
	public class DomainRegistry : Registry
	{
		public DomainRegistry(IEventStore store)
		{
			Scan(a =>
			{
				a.TheCallingAssembly();
				a.AssemblyContainingType<CommandStatus>();
				a.WithDefaultConventions();

				a.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
				a.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
				a.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
				a.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
			});

			For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
			For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
			For<IMediator>().Use<Mediator>();

			For<IEventStore>().Use(store);
		}
	}
}
