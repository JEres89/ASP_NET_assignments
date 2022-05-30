using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_NET_assignments.Models
{
	[Table("PeopleDataTable")]
	[BindProperties]
	public class Person : I_DBformData
	{

		[Display(Name = "Person ID")]
		[Key]
		[Editable(false)]
		public int Id { get; set; }

		[Display(Name = "Full name")]
		[Required]
		public string Name {
			get; set;
		}

		[Display(Name = "Phone number")]
		[Required]
		[Phone]
		public string Phonenumber {
			get; set;
		}

		[Display(Name = "City of Residence")]
		[Required]
		public string City {
			get; set;
		}

		public string[] StringifyValues {
			get {
				return new string[] { Id.ToString(), Name, Phonenumber, City };
			}
		}

		public static string[] stringifyNames { get; } = new string[] {
			"Id", "Name", "Phonenumber", "City" };
		public string[] StringifyNames { get => stringifyNames; }

		public static string[] stringifyDisplayNames { get; } = new string[] {
			"Person ID", "Full name", "Phone number", "City of Residence" };
		public string[] StringifyDisplayNames { get => stringifyDisplayNames; }

		public Person()
		{
			//Id = VirtualDatabase.RandId;
		}
		private Person(int id, string name, string phonenumber, string city)
		{
			Id = id;
			Name = name;
			Phonenumber = phonenumber;
			City = city;
		}
		public Person(string name, string phonenumber, string city) : this(
			VirtualDatabase.RandId, name, phonenumber, city)
		{
		}
	}
}