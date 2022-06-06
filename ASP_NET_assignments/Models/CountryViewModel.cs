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
		public Country GetItem {
			get {
				if(ListEnd)
				{
					return null;
				}
				Country Country = E_countries.Current;
				ListEnd = !E_countries.MoveNext();

				return Country;
			}
		}
		public bool SetNextItem(int id)
		{
			Reset();
			while(E_countries.MoveNext())
			{
				if(E_countries.Current.Id == id)
				{
					return true;
				}
			}
			Reset();
			return false;
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
			}
			else
			{
				Reset();
			}
			ListEnd = false;
		}
		public void AddItem(Country country)
		{
			_database.Countries.Add(country);
			_database.SaveChanges();
			Refresh();
		}

		public bool RemoveItem(int id)
		{
			var success = _database.Countries.Remove(_database.Countries.Find(id));
			_database.SaveChanges();
			Refresh();
			return !_database.Countries.Contains(success.Entity);
		}
	}
}
