using System;
using MediatR;

namespace Domain.Commands
{
	public class ChangeUsersNameCommand : IRequest<CommandStatus>
	{
		public Guid UserID { get; set; }
		public string NewName { get; set; }
	}
}
