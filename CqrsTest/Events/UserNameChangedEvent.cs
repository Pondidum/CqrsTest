using System;
using Ledger;

namespace Domain.Events
{
	public class UserNameChangedEvent : DomainEvent<Guid>
	{
		public string NewName { get; private set; }

		public UserNameChangedEvent(string newName)
		{
			NewName = newName;
		}
	}
}
