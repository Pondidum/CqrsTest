using System;
using System.Collections.Generic;
using Domain.Events;
using Domain.Infrastructure;
using Ledger;

namespace Domain.Services
{
	public class UserService
	{
		private readonly HashSet<string> _knownKeys;
		private readonly Projector _projections;

		public UserService()
		{
			_knownKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			_projections = new Projector();

			_projections.Register<UserCreatedEvent>(e => _knownKeys.Add(e.Key));
		}

		public void Project(DomainEvent<Guid> e)
		{
			_projections.Apply(e);
		}

		public bool IsKeyAvailable(string key)
		{
			return _knownKeys.Contains(key) == false;
		}
	}
}
