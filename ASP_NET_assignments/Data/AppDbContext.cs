using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP_NET_assignments.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<Person> People { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Language> Languages { get; set; }
		public DbSet<PersonLanguage> PersonLanguages { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			string adminroleId = "d13ef063-96a1-48b3-949f-f628ba8a0b7a";
			string userroleId = "43738b5d-1d9a-4337-a794-cc7d6221a3b1";
			string adminuserId = "72f3b5bb-60e5-4c3a-9857-5b8669db0bf4";

			modelBuilder.Entity<AppUser>().HasKey(u => u.Id);
			modelBuilder.Entity<AppUser>().HasMany(u=> u.IdentityUserRoles).WithOne().HasForeignKey(ur => ur.UserId).HasPrincipalKey(u => u.Id);

			modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole[] {
				new IdentityRole{
					Id = adminroleId,
					Name = "Admin",
					NormalizedName = "ADMIN"
				},
				new IdentityRole{
					Id = userroleId,
					Name = "User",
					NormalizedName = "USER"
				}
			});
			modelBuilder.Entity<AppUser>().HasData(new AppUser {
				Id = adminuserId,
				UserName = "MasterAdmin",
				NormalizedUserName = "MASTERADMIN",
				FirstName =	"Jens",
				LastName = "Eresund",
				PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "mASTERaDMIN")
			});
			modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> {
				RoleId = adminroleId,
				UserId = adminuserId
			});

			modelBuilder.Entity<City>()
				.HasAlternateKey(c => c.Name);

			modelBuilder.Entity<Person>()
				.HasOne(p => p.City)
				.WithMany(c => c.People)
				.HasForeignKey(p => p.CityName)
				.HasPrincipalKey(c => c.Name);
			modelBuilder.Entity<City>()
				.HasMany(c => c.People)
				.WithOne(p => p.City);

			modelBuilder.Entity<Country>()
				.HasAlternateKey(cr => cr.Name);
			modelBuilder.Entity<City>()
				.HasOne(c => c.Country)
				.WithMany(cr => cr.Cities)
				.HasForeignKey(c => c.CountryName)
				.HasPrincipalKey(cr => cr.Name);
			modelBuilder.Entity<Country>()
				.HasMany(cr => cr.Cities)
				.WithOne(c => c.Country);

			modelBuilder.Entity<PersonLanguage>()
				.HasKey(pl => new { pl.PersonId, pl.LanguageId });
			modelBuilder.Entity<PersonLanguage>()
				.HasOne(pl => pl.Speaker)
				.WithMany(s => s.PersonLanguages)
				.HasForeignKey(pl => pl.PersonId);
			modelBuilder.Entity<PersonLanguage>()
				.HasOne(pl => pl.Language)
				.WithMany(l => l.PersonLanguages)
				.HasForeignKey(pl => pl.LanguageId);


			var pSeedData = VirtualDatabase.GetSeedData(new Person());
			var cSeedData = VirtualDatabase.GetSeedData(new City());
			var crSeedData = VirtualDatabase.GetSeedData(new Country());
			var lSeedData = VirtualDatabase.GetSeedData(new Language());

			modelBuilder.Entity<Person>().HasData(pSeedData);
			modelBuilder.Entity<City>().HasData(cSeedData);
			modelBuilder.Entity<Country>().HasData(crSeedData);
			modelBuilder.Entity<Language>().HasData(lSeedData);

			List<PersonLanguage> plSeedData = VirtualDatabase.GetPLSeedData(pSeedData, lSeedData);

			modelBuilder.Entity<PersonLanguage>().HasData(plSeedData);
			
		}
	}
}
