using System;
using System.Linq;
using Domain;
using Domain.Commands;
using Domain.Events;
using Domain.Infrastructure;
using Domain.Services;
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
		public void You_cant_create_duplicate_entries()
		{
			var store = new InMemoryEventStore();
			var container = new Container(new TestRegistry(store));

			container.GetInstance<ServiceProjections>().Configure();
			container.GetInstance<ReadModelProjections>().Configure();

			var mediator = container.GetInstance<IMediator>();

			mediator.Send(new CreateUserCommand
			{
				Key = "001",
				Name = "Andy"
			}).ShouldBe(CommandStatus.Accepted);

			Should.Throw<KeyInUseException>(() =>
			{
				mediator.Send(new CreateUserCommand
				{
					Key = "001",
					Name = "Dave"
				});
			});
		}


		[Fact]
		public void When_testing_something()
		{
			var store = new InMemoryEventStore();
			var container = new Container(new TestRegistry(store));

			container.GetInstance<ServiceProjections>().Configure();
			container.GetInstance<ReadModelProjections>().Configure();

			var users = container.GetInstance<AllUsers>();

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
