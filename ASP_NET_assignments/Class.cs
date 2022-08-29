using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ASP_NET_assignments
{
	public class RequestResponseLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			_next = next;
			_logger = loggerFactory
					  .CreateLogger<RequestResponseLoggingMiddleware>();
        }
		public async Task Invoke(HttpContext context)
		{
			Console.WriteLine("log");
			//await LogRequest(context);
			//await LogResponse(context);
			await _next(context);
			Console.WriteLine("log");
		}

		private async Task LogRequest(HttpContext context) { }
		private async Task LogResponse(HttpContext context) { }
	}
	public static class RequestResponseLoggingMiddlewareExtensions
	{
		public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
		}
	}
}
