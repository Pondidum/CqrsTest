using System;
using Domain.Events;
using Ledger;

namespace Domain
{
	public class UserAggregate : AggregateRoot<Guid>
	{
		public string Name { get; private set; }

		public static UserAggregate Blank()
		{
			return new UserAggregate();
		}

		private UserAggregate()
		{
		}

		public UserAggregate(string name)
		{
			ApplyEvent(new UserCreatedEvent(name));
		}

		public void ChangeName(string newName)
		{
			ApplyEvent(new UserNameChangedEvent(newName));
		}


		private void Handle(UserCreatedEvent e)
		{
			Name = e.Name;
		}

		private void Handle(UserNameChangedEvent e)
		{
			Name = e.NewName;
		}
	}
}
