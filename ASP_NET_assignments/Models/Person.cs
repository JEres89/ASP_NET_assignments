using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public class Person
	{
		[Display(Name = "Person ID")]
		[Key]
		public int Id {
			get; set;
		}
		[Display(Name ="Full name")]
		[Required]
		public string Name {
			get; set;
		}
		[Display(Name = "Phonenumber")]
		[Required]
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
			"Person ID", "Full name", "Phonenumber", "City of Residence" };
		//Array.ConvertAll(
		//(DisplayAttribute[])Attribute.GetCustomAttributes(
		//	Assembly.GetAssembly(Type.GetType("ASP_NET_assignments.Models.Person")), 
		//	typeof (DisplayAttribute)), 
		//attribute =>  attribute.Name );

		public Person(int id, string name, string phonenumber, string city)
		{
			Id = id;
			Name = name;
			Phonenumber = phonenumber;
			City = city;
		}
	}
}
