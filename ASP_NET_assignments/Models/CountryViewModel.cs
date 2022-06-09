using ASP_NET_assignments.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public class CountryViewModel : I_DataViewModel<Country>
	{
		private AppDbContext _database;
		private DbSet<Country> countriesData;
		private readonly List<Country> selectedCountriesData;
		private readonly SearchModel<DbSet<Country>, Country> searchModel;
		private IEnumerator<Country> E_countries;

		public string[] ColumnNames { get; set; }
		public bool ListEnd { get; private set; } = false;
		public Country GetNextItem {
			get {
				if(ListEnd = !E_countries.MoveNext())
				{
					return null;
				}
				Country country = E_countries.Current;
				country.setContext(_database);

				return country;
			}
		}
		public Country GetItem(int id)
		{
			Country c= countriesData.Include(c => c.Cities).FirstOrDefault(c => c.Id == id);
			c?.setContext(_database);
			return c;
		}
		public CountryViewModel(AppDbContext dbContext)
		{
			_database = dbContext;
			_database.Countries.Include(c => c.Cities).ToList();
			countriesData = _database.Countries;
			searchModel = new SearchModel<DbSet<Country>, Country>(countriesData);
			selectedCountriesData = searchModel.Result;
			Reset();
		}
		private void Refresh()
		{
			if(searchModel.RenewSearch())
			{
				E_countries = selectedCountriesData.GetEnumerator();
				ListEnd = false;
			}
			else
			{
				Reset();
			}
		}
		private void Reset()
		{
			E_countries = countriesData.AsEnumerable().GetEnumerator();
			ListEnd = false;
			searchModel.ClearSearch();
		}
		public void Search(string value)
		{
			if(searchModel.Search(value))
			{
				E_countries = selectedCountriesData.GetEnumerator();
				ListEnd = false;
			}
			else
			{
				Reset();
			}
		}
		public void AddItem(Country country)
		{
			_database.Countries.Add(country);
			_database.SaveChanges();
			Refresh();
		}

		public bool RemoveItem(int id)
		{
			var country = _database.Countries.Find(id);
			if(country == null)
			{
				return false;
			}
			var success = _database.Countries.Remove(country);
			_database.SaveChanges();
			Refresh();
			return !_database.Countries.Contains(success.Entity);
		}
	}
}
