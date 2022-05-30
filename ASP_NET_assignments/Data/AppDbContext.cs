using ASP_NET_assignments.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_assignments.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<Person> People { get; set; }
		//public DbSet<City> Cities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var seedData = VirtualDatabase.GetDatabase(null).data;

			modelBuilder.Entity<Person>().HasData(seedData.Values);
			
		}
	}
}
