using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_assignments.Models
{
	[BindProperties]
	public class Person
	{
		private readonly int id;
		[Display(Name = "Person ID")]
		[Key]
		[Editable(false)]
		public int Id { get => id; }

		[Display(Name ="Full name")]
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
		
		public static string[] StringifyNames = new string[] {
			"Id", "Name", "Phonenumber", "City" };
		public static string[] StringifyDisplayNames = new string[] {
			"Person ID", "Full name", "Phone number", "City of Residence" };
		//Array.ConvertAll(
		//(DisplayAttribute[])Attribute.GetCustomAttributes(
		//	Assembly.GetAssembly(Type.GetType("ASP_NET_assignments.Models.Person")), 
		//	typeof (DisplayAttribute)), 
		//attribute =>  attribute.Name );

		public Person()
		{
			id = VirtualDatabase.RandId;
		}
		private Person(int id, string name, string phonenumber, string city)
		{
			this.id = id;
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
