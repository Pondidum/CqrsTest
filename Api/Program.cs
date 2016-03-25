using System;
using Domain.Services;
using Ledger.Stores;
using Microsoft.Owin.Hosting;
using StructureMap;

namespace Api
{
	class Program
	{
		static void Main(string[] args)
		{
			var url = "http://localhost:3030";

			var store = new InMemoryEventStore();
			var container = new Container(new ApiRegistry(store));

			var sp = container.GetInstance<ServiceProjections>();
			sp.Configure();

			var api = container.GetInstance<Api>();

			using (WebApp.Start(url, app => api.Configuration(app)))
			{
				Console.WriteLine("Api started, listening on {0}", url);
				Console.WriteLine("Press any key to exit...");

				Console.ReadKey();
			}
		}
	}
	
}
