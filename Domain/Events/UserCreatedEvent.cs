using System;
using Ledger;

namespace Domain.Events
{
	public class UserCreatedEvent : DomainEvent<Guid>
	{
		public string Key { get; private set; }
		public string Name { get; private set; }

		public UserCreatedEvent(string key, string name)
		{
			Key = key;
			Name = name;
		}
	}
}
