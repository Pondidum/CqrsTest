using System;
using Domain.Commands;
using Ledger;
using MediatR;

namespace Domain.CommandHandlers
{
	public class ChangeUsersNameCommandHandler : IRequestHandler<ChangeUsersNameCommand, CommandStatus>
	{
		private readonly AggregateStore<Guid> _userStore;

		public ChangeUsersNameCommandHandler(AggregateStore<Guid> userStore)
		{
			_userStore = userStore;
		}

		public CommandStatus Handle(ChangeUsersNameCommand message)
		{
			var user = _userStore.Load("Users", message.UserID, UserAggregate.Blank);
			user.ChangeName(message.NewName);

			_userStore.Save("Users", user);

			return CommandStatus.Accepted;
		}
	}
}
