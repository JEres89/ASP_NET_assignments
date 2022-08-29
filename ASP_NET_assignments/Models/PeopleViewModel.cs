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
		private List<Person> peopleData;
		private readonly List<Person> selectedPeopleData;
		private readonly SearchModel<DbSet<Person>, Person> searchModel;
		private IEnumerator<Person> E_people;

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

		public List<Person> SelectedPeopleData { 
			get => searchModel.HasSearched ? selectedPeopleData : peopleData; 
		}

		public Person GetItem(int id)
		{
			Person p= peopleData.Find(p => p.Id == id);
			//p?.setContext(_database);
			return p;
		}

		public PeopleViewModel(AppDbContext dbContext) : this(dbContext, null){ }
		public PeopleViewModel(AppDbContext dbContext, string searchValue)
		{
			_database = dbContext;
			peopleData = _database.People.Include(p => p.PersonLanguages).ThenInclude(pl => pl.Language).ToList();
			searchModel = new SearchModel<DbSet<Person>, Person>(_database.People);
			selectedPeopleData = searchModel.Result;
			Search(searchValue);
		}
		private void Refresh()
		{
			if(searchModel.RenewSearch())
			{	
				E_people = SelectedPeopleData.GetEnumerator();
				ListEnd = false;
			}
			else
			{
				Reset();
			}
		}
		private void Reset()
		{
			E_people = peopleData.GetEnumerator();
			ListEnd = false;
			searchModel.ClearSearch();
		}
		public void Search(string value)
		{
			if(searchModel.Search(value))
			{
				E_people = SelectedPeopleData.GetEnumerator();
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
			var person = peopleData.Find(p => p.Id == id);
			if(person == null)
			{
				return false;
			}
			var success = _database.People.Remove(person);

			_database.SaveChanges();
			Refresh();
			return !_database.People.Contains(success.Entity);
		}
	}
}
