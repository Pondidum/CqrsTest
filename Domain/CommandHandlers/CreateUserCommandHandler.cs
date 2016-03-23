using System;
using Domain.Commands;
using Domain.Services;
using Ledger;
using MediatR;

namespace Domain.CommandHandlers
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommandStatus>
	{
		private readonly AggregateStore<Guid> _userStore;
		private readonly UserService _userService;

		public CreateUserCommandHandler(AggregateStore<Guid> userStore, UserService userService)
		{
			_userStore = userStore;
			_userService = userService;
		}

		public CommandStatus Handle(CreateUserCommand message)
		{
			var user = new UserAggregate(_userService, message.Key, message.Name);

			_userStore.Save("Users", user);

			return CommandStatus.Accepted;
		}
	}
}
