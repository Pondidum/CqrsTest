using MediatR;

namespace Domain.Commands
{
	public class CreateUserCommand : IRequest<CommandStatus>
	{
		public string Key { get; set; }
		public string Name { get; set; }
	}
}
