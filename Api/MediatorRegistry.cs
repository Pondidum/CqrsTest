using Domain;
using MediatR;
using StructureMap;
using StructureMap.Graph;

namespace Api
{
	public class MediatorRegistry : Registry
	{
		public MediatorRegistry()
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
		}
	}
}
