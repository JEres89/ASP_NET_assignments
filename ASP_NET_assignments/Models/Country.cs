using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ASP_NET_assignments.Models
{
	[Table("Countries")]
	[BindProperties]
	public class Country : I_DbDataModel<Country>
	{
		[Display(Name = "Country ID")]
		[Key]
		[Editable(false)]
		override public int Id { get; set; }

		[Display(Name = "Country name")]
		[Required]
		public string Name { get; set; }

		[Display(Name = "Cities in Nation")]
		public List<City> Cities { get; set; } = new List<City>();

		override public string[] StringifyValues {
			get {
				string cities = new Func<string>(() => {
					StringBuilder s = new StringBuilder();
					foreach (City c in Cities)
					{
						if (s.Length != 0)
						{
							s.Append(", ");
						}
						s.Append(c.Name);
					}
					return s.ToString();
				})();

				return new string[] { Id.ToString(), Name,
					cities };
			}
		}
		static Country()
		{
			StringifyNames = new string[] {
			"Id", "Name" };
			StringifyDisplayNames = new string[] {
			"Country ID", "Country name", "Cities in Nation" };
			TableName = "Countries";
		}
		public Country()
		{
		}
		private Country(int id, string name)
		{
			Id = id;
			Name = name;
		}
		public Country(string name) : this(
			VirtualDatabase.RandId, name)
		{
		}

		public override Country MakeInstance(string[] values)
		{
			if(values.Length < 1)
			{
				return null;
			}
			return new Country(values[0]);
		}
	}
}
