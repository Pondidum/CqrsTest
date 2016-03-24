using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Api
{
	class Program
	{
		static void Main(string[] args)
		{
			var url = "http://localhost:3030";

			using (WebApp.Start<Api>(url))
			{
				Console.WriteLine("Api started, listening on {0}", url);
				Console.WriteLine("Press any key to exit...");

				Console.ReadKey();
			}
		}
	}
}
