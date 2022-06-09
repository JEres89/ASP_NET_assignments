using ASP_NET_assignments.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ASP_NET_assignments.Models
{
	[Table("Languages")]
	[BindProperties]
	public class Language : I_DbDataModel<Language>
	{
		[Display(Name = "Language ID")]
		[Key]
		[Editable(false)]
		override public int Id { get; set; }

		[Display(Name = "Language name")]
		[Required]
		public string Name { get; set; }

		[Display(Name = "Speakers")]
		public List<PersonLanguage> PersonLanguages { get; set; } = new List<PersonLanguage>();

		override public string[] StringifyValues {
			get {
				string people = new Func<string>(() => {
					StringBuilder s = new StringBuilder();

					foreach (PersonLanguage p in PersonLanguages)
					{
						if (s.Length != 0)
						{
							s.Append(", ");
						}
						s.Append(_database.People.Find(p.PersonId).Name);
					}
					return s.ToString();
				})();

				return new string[] { Id.ToString(), Name,
					people };
			}
		}
		private AppDbContext _database;
		override internal void setContext(AppDbContext database)
		{
			_database = database;
		}

		static Language()
		{
			StringifyNames = new string[] {
			"Id", "Name" };
			StringifyDisplayNames = new string[] {
			"Language ID", "Language name", "Speakers" };
			TableName = "Languages";
		}
		public Language()
		{
		}
		private Language(int id, string name)
		{
			Id = id;
			Name = name;
		}
		public Language(string name) : this(
			VirtualDatabase.RandId, name)
		{
		}

		public override Language MakeInstance(string[] values)
		{
			if(values.Length < 1)
			{
				return null;
			}
			return new Language(values[0]);
		}
	}
}
