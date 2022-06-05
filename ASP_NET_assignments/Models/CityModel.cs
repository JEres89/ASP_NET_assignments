using ASP_NET_assignments.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public class CityModel : I_DataViewModel<City>
	{
		private AppDbContext _database;
		private string[] postNames;
		private DbSet<City> citiesData;
		private readonly List<City> selectedCitiesData;
		private readonly SearchModel<DbSet<City>, City> searchModel;
		private IEnumerator<City> E_cities;
		private bool listEnd = false;

		public string[] PostNames { get => postNames; set => postNames = value; }
		public bool ListEnd {
			get => listEnd;
			private set => listEnd = value;
		}
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
		public CityModel(AppDbContext dbContext)
		{
			_database = dbContext;
			citiesData = _database.Cities;
			searchModel = new SearchModel<DbSet<City>, City>(ref citiesData);
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
			var success = _database.People.Remove(_database.People.Find(id));
			_database.SaveChanges();
			Refresh();
			return success.Entity == null;
		}

	}
}
