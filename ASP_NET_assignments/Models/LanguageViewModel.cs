using ASP_NET_assignments.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public class LanguageViewModel : I_DataViewModel<Language>
	{
		private AppDbContext _database;
		private DbSet<Language> languagesData;
		private readonly List<Language> selectedLanguagesData;
		private readonly SearchModel<DbSet<Language>, Language> searchModel;
		private IEnumerator<Language> E_languages;

		public string[] ColumnNames { get; set; }
		public bool ListEnd { get; private set; } = false;
		public Language GetNextItem {
			get {
				if(ListEnd = !E_languages.MoveNext())
				{
					return null;
				}
				Language language = E_languages.Current;
				language.setContext(_database);

				return language;
			}
		}
		public Language GetItem(int id)
		{
			Language l= languagesData.Include(l => l.PersonLanguages).FirstOrDefault(l => l.Id == id);
			l?.setContext(_database);
			return l;
		}
		public LanguageViewModel(AppDbContext dbContext)
		{
			_database = dbContext;
			_database.Languages.Include(c => c.PersonLanguages).ToList();
			languagesData = _database.Languages;
			searchModel = new SearchModel<DbSet<Language>, Language>(languagesData);
			selectedLanguagesData = searchModel.Result;
			Reset();
		}
		private void Refresh()
		{
			if(searchModel.RenewSearch())
			{
				E_languages = selectedLanguagesData.GetEnumerator();
				ListEnd = false;
			}
			else
			{
				Reset();
			}
		}
		private void Reset()
		{
			E_languages = languagesData.AsEnumerable().GetEnumerator();
			ListEnd = false;
			searchModel.ClearSearch();
		}
		public void Search(string value)
		{
			if(searchModel.Search(value))
			{
				E_languages = selectedLanguagesData.GetEnumerator();
				ListEnd = false;
			}
			else
			{
				Reset();
			}
		}
		public void AddItem(Language language)
		{
			_database.Languages.Add(language);
			_database.SaveChanges();
			Refresh();
		}

		public bool RemoveItem(int id)
		{
			var language = languagesData.Find(id);
			if(language == null)
			{
				return false;
			}
			var success = languagesData.Remove(language);
			
			_database.SaveChanges();
			Refresh();
			return !_database.Languages.Contains(success.Entity);
		}
	}
}
