using System.Net;
using System.Threading.Tasks;
using Domain;
using Domain.Commands;
using Domain.Services;
using MediatR;
using Microsoft.Owin;
using Owin;
using Owin.Routing;

namespace Api
{
	public class Api
	{
		private readonly IMediator _mediator;
		private readonly UserService _userService;

		public Api(IMediator mediator, UserService userService)
		{
			_mediator = mediator;
			_userService = userService;
		}

		public void Configuration(IAppBuilder app)
		{
			app.Post("/users/create", UserCreate);
		}

		private async Task UserCreate(IOwinContext context)
		{
			var dto = context.ReadJson<CreateDto>();

			if (_userService.IsKeyAvailable(dto.Key) == false)
			{
				context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
				await context.WriteJson(new { Message = $"The key {dto.Key} is already in use." });
				return;
			}

			var result = _mediator.Send(new CreateUserCommand
			{
				Key = dto.Key,
				Name = dto.Name
			});

			if (result != CommandStatus.Accepted)
				context.Response.StatusCode = (int) HttpStatusCode.BadRequest;

		}

		public class CreateDto
		{
			public string Key { get; set; }
			public string Name { get; set; }
		}
	}
}
