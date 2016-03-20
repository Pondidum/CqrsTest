using System;
using Domain.Commands;
using Ledger;
using MediatR;

namespace Domain.CommandHandlers
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommandStatus>
	{
		private readonly AggregateStore<Guid> _userStore;

		public CreateUserCommandHandler(AggregateStore<Guid> userStore)
		{
			_userStore = userStore;
		}

		public CommandStatus Handle(CreateUserCommand message)
		{
			var user = new UserAggregate(message.Key, message.Name);

			_userStore.Save("Users", user);

			return CommandStatus.Accepted;
		}
	}
}
