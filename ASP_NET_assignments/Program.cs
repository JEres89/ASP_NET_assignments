using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_assignments
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//(string, int, string) asd = ("asd", 123, "asd");
			
			//string s = asd.GetType().BaseType.Name;

			//asd.Item2=0;
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging(logBuilder => logBuilder.SetMinimumLevel(LogLevel.Trace))
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
