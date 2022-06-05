using ASP_NET_assignments.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ASP_NET_assignments.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<Person> People { get; set; }
		public DbSet<City> Cities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<City>().HasAlternateKey(c => c.Name);
			modelBuilder.Entity<Person>().HasOne(p => p.City).WithMany(c => c.People).HasForeignKey(p => p.CityName).HasPrincipalKey(c => c.Name);
			modelBuilder.Entity<City>().HasMany(c => c.People).WithOne(p => p.City);

			var pSeedData = VirtualDatabase.GetSeedData(new Person());
			var cSeedData = VirtualDatabase.GetSeedData(new City());

			modelBuilder.Entity<Person>().HasData(pSeedData);
			modelBuilder.Entity<City>().HasData(cSeedData);
		}
	}
}
