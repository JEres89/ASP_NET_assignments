using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_assignments
{
	public class Startup
	{
		private readonly IConfiguration Configuration;

		public Startup(IConfiguration config)
		{
			Configuration = config;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options => options.AddPolicy(name: "AllowCORS", policy => policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowCredentials().AllowAnyMethod().AllowAnyHeader()));
			services.AddMvc();
			services.AddDistributedMemoryCache();
			services.AddSession();
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddIdentity<AppUser, IdentityRole>(options => { 
				options.SignIn.RequireConfirmedAccount = false;})
				.AddDefaultUI()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<AppDbContext>();
			//services.AddSwaggerGen();
			//services.AddLogging();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				//app.UseSwagger();
				//app.UseSwaggerUI();
				//app.UseRequestResponseLogging();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseCors("AllowCORS");
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}"
					);
				//endpoints.MapControllerRoute(
				//	"api_default",
				//	"api/{controller=PeopleAPI}/{action=GetPeople}"
				//	);
				endpoints.MapControllerRoute(
					"people",
					"People",
					new
					{ controller = "People", action = "Index" }
					);
				endpoints.MapControllerRoute(
					"game",
					"GuessingGame",
					new
					{ controller = "Game", action = "GuessingGame" }
					);
				endpoints.MapControllerRoute(
					"guess",
					"Game/GuessingGame/{guess:int}",
					new
					{ controller = "Game", action = "GuessingGame" }
					);
				endpoints.MapControllerRoute(
					"doctor",
					"FeverCheck",
					new
					{ controller = "Doctor", action = "FeverCheck" }
					);
				endpoints.MapRazorPages();
				//endpoints.MapControllerRoute(
				//	"measure",
				//	"Doctor/FeverCheck/{temp:float}",
				//	new
				//	{ controller = "Doctor", action = "FeverCheck" }
				//	);


				//endpoints.MapPost(
				//	"Doctor/FeverCheck",

				//	new RequestDelegate
				//	(
				//		context =>
				//		{
				//			float temp;
				//			if(float.TryParse(context.Request.Form["temp"], out temp))
				//			{
				//				// match route???
				//			}
				//			else
				//			{
				//				// do not match route???
				//			}
				//			return Task.CompletedTask;
				//		}
				//	)
				//);
			});
		}
	}
}
