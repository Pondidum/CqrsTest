using System;
using Domain.Events;
using Domain.Services;
using Ledger;

namespace Domain
{
	public class UserAggregate : AggregateRoot<Guid>
	{
		public string Key { get; private set; }
		public string Name { get; private set; }

		public static UserAggregate Blank()
		{
			return new UserAggregate();
		}

		private UserAggregate()
		{
		}

		public UserAggregate(UserService userService, string key, string name)
		{
			if (userService.IsKeyAvailable(key) == false)
				throw new KeyInUseException(key);

			ApplyEvent(new UserCreatedEvent(Guid.NewGuid(), key, name));
		}

		public void ChangeName(string newName)
		{
			ApplyEvent(new UserNameChangedEvent(newName));
		}


		private void Handle(UserCreatedEvent e)
		{
			ID = e.AggregateID;
			Key = e.Key;
			Name = e.Name;
		}

		private void Handle(UserNameChangedEvent e)
		{
			Name = e.NewName;
		}
	}
}
