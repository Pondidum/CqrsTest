using System;
using Ledger;

namespace Domain.Events
{
	public class UserCreatedEvent : DomainEvent<Guid>
	{
		public string Name { get; private set; }

		public UserCreatedEvent(string name)
		{
			Name = name;
		}
	}
}
