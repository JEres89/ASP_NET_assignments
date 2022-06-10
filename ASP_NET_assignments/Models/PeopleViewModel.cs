using ASP_NET_assignments.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace ASP_NET_assignments.Models
{
	public class PeopleViewModel : I_DataViewModel <Person>
	{
		//private static PeopleViewModel cache;
		private AppDbContext _database;
		private DbSet<Person> peopleData;
		private readonly List<Person> selectedPeopleData;
		private readonly SearchModel<DbSet<Person>, Person> searchModel;
		private IEnumerator<Person> E_people;

		//public List<Person> PeopleData {
		//	get {
		//		if(searchModel.HasSearched)
		//		{
		//			return selectedPeopleData;
		//		}
		//		return peopleData.ToList();
		//	}
		//}
		public string[] ColumnNames { get; set; }
		public bool ListEnd { get; private set; } = false;
		public Person GetNextItem {
			get {
				if(ListEnd = !E_people.MoveNext())
				{
					return null;
				}
				Person person = E_people.Current;
				person.setContext(_database);

				return person;
			}
		}

		public Person GetItem(int id)
		{
			Person p= peopleData.Include(p => p.PersonLanguages).FirstOrDefault(p => p.Id == id);
			p?.setContext(_database);
			return p;
		}

		//public static PeopleViewModel GetSessionModel(AppDbContext dbContext)
		//{
		//	if(cache == null)
		//	{
		//		cache = new PeopleViewModel(dbContext);
		//	}
		//	else
		//	{
		//		if(cache._database != dbContext)
		//		{
		//			cache._database = dbContext;
		//			cache.searchModel.dataSet = cache.peopleData = cache._database.People;
		//		}
		//		else cache.Reset();
		//	}
		//	return cache;
		//}

		public PeopleViewModel(AppDbContext dbContext)
		{
			_database = dbContext;
			_database.People.Include(p => p.PersonLanguages).ToList();
			peopleData = _database.People;
			searchModel = new SearchModel<DbSet<Person>, Person>(peopleData);
			selectedPeopleData = searchModel.Result;
			Reset();
		}
		private void Refresh()
		{
			if(searchModel.RenewSearch())
			{	
				E_people = selectedPeopleData.GetEnumerator();
				ListEnd = false;
			}
			else
			{
				Reset();
			}
		}
		private void Reset()
		{
			E_people = peopleData.AsEnumerable().GetEnumerator();
			ListEnd = false;
			searchModel.ClearSearch();
		}
		public void Search(string value)
		{
			if(searchModel.Search(value))
			{
				E_people = selectedPeopleData.GetEnumerator();
				ListEnd = false;
			}
			else
			{
				Reset();
			}
		}

		public void AddItem(Person person)
		{
			_database.People.Add(person);
			_database.SaveChanges();
			Refresh();
		}

		public bool RemoveItem(int id)
		{
			var person = peopleData.Find(id);
			if(person == null)
			{
				return false;
			}
			var success = peopleData.Remove(person);

			_database.SaveChanges();
			Refresh();
			return !_database.People.Contains(success.Entity);
		}
	}
}
