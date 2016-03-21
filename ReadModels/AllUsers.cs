using System;
using System.Collections.Generic;
using Domain.Events;
using Ledger;

namespace ReadModels
{
	public class AllUsers
	{
		public Dictionary<Guid, string> Users { get; }
		 
		private readonly Dictionary<Type, Action<DomainEvent<Guid>>> _projections;

		public AllUsers()
		{
			_projections = new Dictionary<Type, Action<DomainEvent<Guid>>>();

			Users = new Dictionary<Guid, string>();

			Register<UserCreatedEvent>(e => Users[e.AggregateID] = e.Name);
			Register<UserNameChangedEvent>(e => Users[e.AggregateID] = e.NewName);
		}

		private void Register<TEvent>(Action<TEvent> projection) where TEvent : DomainEvent<Guid>
		{
			_projections[typeof(TEvent)] = e => projection((TEvent)e);
		}

		public void Project(DomainEvent<Guid> e)
		{
			Action<DomainEvent<Guid>> projection;

			if (_projections.TryGetValue(e.GetType(), out projection))
				projection(e);
		}
	}
}
