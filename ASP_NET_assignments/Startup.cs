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

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddDistributedMemoryCache();
			services.AddSession();
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddIdentity<AppUser, IdentityRole>(options => { 
				options.SignIn.RequireConfirmedAccount = false;})
				.AddDefaultUI()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<AppDbContext>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}"
					);
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
