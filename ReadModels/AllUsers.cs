using System;
using System.Collections.Generic;
using Domain.Events;
using Domain.Infrastructure;
using Ledger;

namespace ReadModels
{
	public class AllUsers
	{
		public IEnumerable<string> Names => _users.Values;
		 
		private readonly Dictionary<Guid, string> _users;
		private readonly Projector _projections;

		public AllUsers()
		{
			_projections = new Projector();
			_users = new Dictionary<Guid, string>();

			_projections.Register<UserCreatedEvent>(e => _users[e.AggregateID] = e.Name);
			_projections.Register<UserNameChangedEvent>(e => _users[e.AggregateID] = e.NewName);
		}

		public void Project(DomainEvent<Guid> e)
		{
			_projections.Apply(e);
		}
	}
}
