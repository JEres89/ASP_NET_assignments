using ASP_NET_assignments.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ASP_NET_assignments.Models
{
	[Table("Cities")]
	[BindProperties]
	public class City : I_DbDataModel<City>
	{
		[Display(Name = "City ID")]
		[Key]
		[Editable(false)]
		override public int Id { get; set; }

		[Display(Name = "City name")]
		[Required]
		public string Name { get; set; }

		[Display(Name = "Residents")]
		public List<Person> People { get; set; } = new List<Person>();

		[Display(Name = "Country")]
		[Required]
		public string CountryName { get; set; }
		[ForeignKey("CountryName")]
		public Country Country { get; set; }

		override public string[] StringifyValues {
			get {
				string people = new Func<string>(() => {
					StringBuilder s = new StringBuilder();
					foreach (Person p in People)
					{
						if (s.Length != 0)
						{
							s.Append(", ");
						}
						s.Append(p.Name);
					}
					return s.ToString();})();

				return new string[] { Id.ToString(), Name,
					people, CountryName };
			}
		}
		private AppDbContext _database;
		override internal void setContext(AppDbContext database)
		{
			_database = database;
		}

		static City()
		{
			StringifyNames = new string[] {
			"Id", "Name", "CountryName" };
			StringifyDisplayNames = new string[] {
			"City ID", "City name", "Residents", "Country" };
			TableName = "Cities";
		}
		//public static new string[] StringifyNames { get; } = new string[] {
		//	"Id", "Name", "People" };
		//public string[] StringifyNames { get => stringifyNames; }

		//public static new string[] StringifyDisplayNames { get; } = new string[] {
		//	"City ID", "City name", "Residents" };
		//public string[] StringifyDisplayNames { get => stringifyDisplayNames; }
		public City()
		{
		}
		private City(int id, string name, string countryName)
		{
			Id = id;
			Name = name;
			CountryName = countryName;
		}
		public City(string name, string countryName) : this(
			VirtualDatabase.RandId, name, countryName)
		{
		}

		public override City MakeInstance(string[] values)
		{
			if(values.Length < 2)
			{
				return null;
			}
			return new City(values[0], values[1]);
		}
	}
}
