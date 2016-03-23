using System;
using Ledger;

namespace Domain.Events
{
	public class UserCreatedEvent : DomainEvent<Guid>
	{
		public string Key { get; private set; }
		public string Name { get; private set; }

		public UserCreatedEvent(Guid id, string key, string name)
		{
			AggregateID = id;
			Key = key;
			Name = name;
		}
	}
}
