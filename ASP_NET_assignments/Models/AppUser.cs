using ASP_NET_assignments.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public class AppUser : IdentityUser
	{
		public AppUser()
		{
		}
		//[Key]
		//[Editable(false)]
		//public override string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Birthdate { get; set; }
		public int UserDetailsColumns { get; set; }
		public List<IdentityUserRole<string>> IdentityUserRoles { get; set; }

		//public override string Id { get => base.Id; set => base.Id = value; }
		//public override string UserName { get => base.UserName; set => base.UserName = value; }
		//public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
		//public override string Email { get => base.Email; set => base.Email = value; }
		//public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
		//public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
		//public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
		//public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
		//public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
		//public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
		//public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }
		//public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }
		//public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }
		//public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }
		//public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }

	}
}
