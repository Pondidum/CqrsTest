using System;
using System.Linq;
using Domain;
using Domain.Commands;
using Domain.Events;
using Ledger;
using Ledger.Stores;
using MediatR;
using Shouldly;
using StructureMap;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
	public class Scratchpad
	{
		private readonly ITestOutputHelper _output;

		public Scratchpad(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void When_testing_something()
		{
			Action<DomainEvent<Guid>> projection = e =>
			{
				
			};

			var store = new InMemoryEventStore();
			var container = new Container(new DomainRegistry(new ProjectionStore(store, projection)));

			var mediator = container.GetInstance<IMediator>();

			var response = mediator.Send(new CreateUserCommand
			{
				Key = "001",
				Name = "Andy"
			});

			response.ShouldBe(CommandStatus.Accepted);
			store.AllEvents.Single().ShouldBeOfType<UserCreatedEvent>();
		}
	}
}
