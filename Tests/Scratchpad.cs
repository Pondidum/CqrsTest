using System;
using System.Linq;
using Domain;
using Domain.Commands;
using Domain.Events;
using Ledger;
using Ledger.Stores;
using MediatR;
using ReadModels;
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
			var users = new AllUsers();

			Action<DomainEvent<Guid>> projection = e =>
			{
				users.Project(e);
			};

			var store = new InMemoryEventStore();
			var container = new Container(new DomainRegistry(new ProjectionStore(store, projection)));

			var mediator = container.GetInstance<IMediator>();

			mediator.Send(new CreateUserCommand
			{
				Key = "001",
				Name = "Andy"
			}).ShouldBe(CommandStatus.Accepted);

			store.AllEvents.Single().ShouldBeOfType<UserCreatedEvent>();
			users.Names.Single().ShouldBe("Andy");

			mediator.Send(new ChangeUsersNameCommand
			{
				UserID = store.AllEvents.Cast<DomainEvent<Guid>>().Single().AggregateID,
				NewName = "Andrew"
			}).ShouldBe(CommandStatus.Accepted);


			users.Names.Single().ShouldBe("Andrew");
		}
	}
}
