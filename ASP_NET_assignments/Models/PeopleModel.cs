using ASP_NET_assignments.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace ASP_NET_assignments.Models
{
	public class PeopleModel : I_DataViewModel <Person>
	{
		//private static PeopleModel cache;
		private AppDbContext _database;
		private string[] postNames;
		private DbSet<Person> peopleData;
		private readonly List<Person> selectedPeopleData;
		private readonly SearchModel<DbSet<Person>, Person> searchModel;
		private IEnumerator<Person> E_people;
		private bool listEnd = false;
		public List<Person> PeopleData {
			get {
				if(searchModel.HasSearched)
				{
					return selectedPeopleData;
				}
				return peopleData.ToList();
			}
		}
		public string[] PostNames { get => postNames; set => postNames =  value ; }

		public bool ListEnd {
			get => listEnd;
			private set => listEnd = value;
		}
		public Person GetItem {
			get {
				if(ListEnd)
				{
					return null;
				}
				Person person = E_people.Current;
				ListEnd = !E_people.MoveNext();

				return person;
			}
		}

		public bool SetNextItem(int id)
		{
			Reset();
			while(E_people.MoveNext())
			{
				if(E_people.Current.Id == id)
				{
					return true;
				}
			}
			Reset();
			return false;
		}

		//public static PeopleModel GetSessionModel(AppDbContext dbContext)
		//{
		//	if(cache == null)
		//	{
		//		cache = new PeopleModel(dbContext);
		//	}
		//	else
		//	{
		//		cache._database = dbContext;
		//		cache.peopleData = cache._database.People;
		//		cache.Refresh();
		//	}
		//	return cache;
		//}

		public PeopleModel(AppDbContext dbContext)
		{
			_database = dbContext;
			//var session = VirtualDatabase.GetDatabase(_databaseId);
			//postNames = session.postNames;
			//PeopleData = session.data;
			//Dictionary<int, Person> searchCollection = new Dictionary<int, Person>();
			peopleData = _database.People;
			searchModel = new SearchModel<DbSet<Person>, Person>(ref peopleData);
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
		}
		public void Search(string value)
		{
			if(searchModel.Search(value))
			{
				E_people = selectedPeopleData.GetEnumerator();
			}
			else
			{
				Reset();
			}
			ListEnd = false;
		}

		public void AddItem(Person person)
		{
			_database.People.Add(person);
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
