using System;
using System.Collections.Generic;
using Domain.Events;
using Domain.Infrastructure;
using Ledger;

namespace Domain.Services
{
	public class UserService
	{
		private static readonly HashSet<string> KnownKeys;
		private static readonly Projector Projections;

		static UserService()
		{
			KnownKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			Projections = new Projector();

			Projections.Register<UserCreatedEvent>(e => KnownKeys.Add(e.Key));
		}

		public static void Project(DomainEvent<Guid> e)
		{
			Projections.Apply(e);
		}

		public static bool IsKeyAvailable(string key)
		{
			return KnownKeys.Contains(key) == false;
		}
	}
}
