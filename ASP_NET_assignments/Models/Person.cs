using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	[Table("PeopleDataTable")]
	[BindProperties]
	public class Person : I_DbDataModel<Person>
	{

		[Display(Name = "Person ID")]
		[Key]
		[Editable(false)]
		override public int Id { get; set; }

		[Display(Name = "Full name")]
		[Required]
		public string Name { get; set; }

		[Display(Name = "Phone number")]
		[Required]
		[Phone]
		public string Phonenumber { get; set; }

		[Display(Name = "City of Residence")]
		[Required]
		public string CityName { get; set; }

		public City City { get; set; }

		override public string[] StringifyValues {
			get {
				return new string[] { Id.ToString(), Name, Phonenumber, CityName };
			}
		}

		public static new string[] StringifyNames { get; } = new string[] {
			"Id", "Name", "Phonenumber", "CityName" };
		//override public string[] StringifyNames { get => stringifyNames; }

		public static new string[] StringifyDisplayNames { get; } = new string[] {
			"Person ID", "Full name", "Phone number", "City of Residence" };
		//override public string[] StringifyDisplayNames { get => stringifyDisplayNames; }

		public Person()
		{
		}
		private Person(int id, string name, string phonenumber, string cityName)
		{
			Id = id;
			Name = name;
			Phonenumber = phonenumber;
			CityName = cityName;
		}
		public Person(string name, string phonenumber, string cityName) : this(
			VirtualDatabase.RandId, name, phonenumber, cityName)
		{
		}

		public override Person MakeInstance(string[] values)
		{
			if(values.Length<3)
			{
				return null;
			}
			return new Person(values[0], values[1], values[2]);
		}

		//public static new IQueryable<Person> Search(DbSet<Person> dataSet, string value)
		//{
		//	return dataSet.Where(row =>
		//		row.Name.Contains(value) || 
		//		row.CityName.Contains(value) ||
		//		row.Phonenumber.Contains(value)
		//	);
		//}
	}
}