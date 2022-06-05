using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ASP_NET_assignments.Models
{
	public class City : I_DbDataModel<City>
	{
		[Display(Name = "City ID")]
		[Key]
		[Editable(false)]
		override public int Id { get; set; }

		[Display(Name = "City name")]
		[Required]
		public string Name { get; set; }

		//[Display(Name = "Country")]
		//[Required]
		//public Country Country { get; set; }

		[Display(Name = "Residents")]
		public List<Person> People { get; set; } = new List<Person>();

		override public string[] StringifyValues {
			get {
				string people = new Func<string>(() => {
					StringBuilder s = new StringBuilder();
					foreach (Person p in People)
					{
						s.Append(p.Name).Append("U+002C");
					}
					return s.ToString();})();

				return new string[] { Id.ToString(), Name,
					people };
			}
		}

		public static new string[] StringifyNames { get; } = new string[] {
			"Id", "Name", "People" };
		//public string[] StringifyNames { get => stringifyNames; }

		public static new string[] StringifyDisplayNames { get; } = new string[] {
			"City ID", "City name", "Residents" };
		//public string[] StringifyDisplayNames { get => stringifyDisplayNames; }
		public City()
		{
		}
		private City(int id, string name)
		{
			Id = id;
			Name = name;
		}
		public City(string name) : this(
			VirtualDatabase.RandId, name)
		{
		}

		public override City MakeInstance(string[] values)
		{
			if(values.Length < 1)
			{
				return null;
			}
			return new City(values[0]);
		}
	}
}
