using ASP_NET_assignments.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ASP_NET_assignments.Models
{
	[Table("PeopleDataTable")]
	[BindProperties]
	public class Person : I_DbDataModel<Person>
	{

		[Display(Name = "Person ID")]
		[Key]
		[Editable(false)]
		public int Id { get; set; }

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

		[ForeignKey("CityName")]
		public City City { get; set; }

		[Display(Name = "Spoken Languages")]
		public List<PersonLanguage> PersonLanguages { get; set; } = new List<PersonLanguage>();

		override public string[] StringifyValues {
			get {
				string languages = new Func<string>(() => {
					StringBuilder s = new StringBuilder();
					foreach (PersonLanguage l in PersonLanguages)
					{
						if (s.Length != 0)
						{
							s.Append(", ");
						}
						s.Append(_database.Languages.Find(l.LanguageId).Name);
					}
					return s.ToString();
				})();
				return new string[] { Id.ToString(), Name, Phonenumber, CityName, languages };
			}
		}
		private AppDbContext _database;
		override internal void setContext(AppDbContext database)
		{
			_database = database;
		}

		static Person()
		{
			StringifyNames = new string[] {
			"Id", "Name", "Phonenumber", "CityName" };
			StringifyDisplayNames = new string[] {
			"Person ID", "Full name", "Phone number", "City of Residence", "Spoken Languages" };
			TableName = "PeopleDataTable";
		}
		//public static new string[] StringifyNames  = new string[] {
		//	"Id", "Name", "Phonenumber", "CityName" };
		//override public string[] StringifyNames { get => stringifyNames; }

		//public static new string[] StringifyDisplayNames { get; } = new string[] {
		//	"Person ID", "Full name", "Phone number", "City of Residence" };
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

		public override Person MakeInstance(int id, string[] values)
		{
			if(values.Length<3)
			{
				return null;
			}
			return new Person(id, values[0], values[1], values[2]);
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