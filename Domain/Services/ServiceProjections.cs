using Domain.Infrastructure;

namespace Domain.Services
{
	public class ServiceProjections
	{
		private readonly Projectionist _projectionist;
		private readonly UserService _service;

		public ServiceProjections(Projectionist projectionist, UserService service)
		{
			_projectionist = projectionist;
			_service = service;
		}

		public void Configure()
		{
			_projectionist.Add(_service.Project);
		}
	}
}
