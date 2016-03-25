using Domain.Infrastructure;

namespace ReadModels
{
	public class ReadModelProjections
	{
		private readonly Projectionist _projectionist;
		private readonly AllUsers _allUsers;

		public ReadModelProjections(Projectionist projectionist, AllUsers allUsers)
		{
			_projectionist = projectionist;
			_allUsers = allUsers;
		}

		public void Configure()
		{
			_projectionist.Add(_allUsers.Project);
		}
	}
}
