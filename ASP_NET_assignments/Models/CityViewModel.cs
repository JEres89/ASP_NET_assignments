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
		public City GetNextItem {
			get {
				if(ListEnd = !E_cities.MoveNext())
				{
					return null;
				}
				City city = E_cities.Current;
				city.setContext(_database);

				return city;
			}
		}
		public City GetItem(int id)
		{
			City c= citiesData.Include(c => c.People).FirstOrDefault(c => c.Id == id);
			c?.setContext(_database);
			return c;
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
				ListEnd = false;
			}
			else
			{
				Reset();
			}
		}
		public void AddItem(City city)
		{
			_database.Cities.Add(city);
			_database.SaveChanges();
			Refresh();
		}

		public bool RemoveItem(int id)
		{
			var city = _database.Cities.Find(id);
			if(city == null)
			{
				return false;
			}
			var success = _database.Cities.Remove(city);
			_database.SaveChanges();
			Refresh();
			return !_database.Cities.Contains(success.Entity);
		}
	}
}
