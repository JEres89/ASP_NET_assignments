using ASP_NET_assignments.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public class CityViewModel : I_DataViewModel<City>
	{
		private AppDbContext _database;
		private DbSet<City> citiesData;
		private readonly List<City> selectedCitiesData;
		private readonly SearchModel<DbSet<City>, City> searchModel;
		private IEnumerator<City> E_cities;

		public string[] ColumnNames { get; set; }
		public bool ListEnd { get; private set; } = false;
		public City GetItem {
			get {
				if(ListEnd)
				{
					return null;
				}
				City city = E_cities.Current;
				ListEnd = !E_cities.MoveNext();

				return city;
			}
		}
		public bool SetNextItem(int id)
		{
			Reset();
			while(E_cities.MoveNext())
			{
				if(E_cities.Current.Id == id)
				{
					return true;
				}
			}
			Reset();
			return false;
		}
		public CityViewModel(AppDbContext dbContext)
		{
			_database = dbContext;
			_database.Cities.Include(c => c.People).ToList();
			citiesData = _database.Cities;
			searchModel = new SearchModel<DbSet<City>, City>(citiesData);
			selectedCitiesData = searchModel.Result;
			Reset();
		}
		private void Refresh()
		{
			if(searchModel.RenewSearch())
			{
				E_cities = selectedCitiesData.GetEnumerator();
				ListEnd = false;
			}
			else
			{
				Reset();
			}
		}
		private void Reset()
		{
			E_cities = citiesData.AsEnumerable().GetEnumerator();
			ListEnd = false;
			searchModel.ClearSearch();
		}
		public void Search(string value)
		{
			if(searchModel.Search(value))
			{
				E_cities = selectedCitiesData.GetEnumerator();
			}
			else
			{
				Reset();
			}
			ListEnd = false;
		}
		public void AddItem(City city)
		{
			_database.Cities.Add(city);
			_database.SaveChanges();
			Refresh();
		}

		public bool RemoveItem(int id)
		{
			var success = _database.Cities.Remove(_database.Cities.Find(id));
			_database.SaveChanges();
			Refresh();
			return !_database.Cities.Contains(success.Entity);
		}
	}
}
