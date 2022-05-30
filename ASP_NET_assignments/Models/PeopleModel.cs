using ASP_NET_assignments.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace ASP_NET_assignments.Models
{
	public class PeopleModel 
	{
		private static PeopleModel cache;

		private AppDbContext _database;
		public string[] postNames;
		private DbSet<Person> peopleData;
		private readonly List<Person> selectedPeopleData;

		private readonly SearchModel<DbSet<Person>, Person> searchModel;
		//public Dictionary<int, Person> PeopleData
		//{
		//	get
		//	{
		//		if(searchModel.HasSearched)
		//		{
		//			return selectedPeopleData;
		//		}
		//		return peopleData;
		//	}
		//	private set => peopleData =  value ;
		//}
		private IEnumerator<Person> E_people;
		private bool listEnd = false;
		public bool ListEnd
		{
			get => listEnd;
			private set => listEnd = value ;
		}
		public Person GetPerson
		{
			get
			{
				if(ListEnd)
				{
					return null;
				}
				Person person = E_people.Current;
				ListEnd = !E_people.MoveNext();

				return person;
			}
		}
		public bool SetPerson(int id)
		{
			Reset();
			while(E_people.Current.Id != id)
			{
				if(!E_people.MoveNext())
				{
					Reset();
					return false;
				}
			}
			return true;
		}

		public static PeopleModel GetSessionModel(AppDbContext dbContext)
		{
			if(cache == null)
			{
				cache = new PeopleModel(dbContext);
			}
			else
			{
				cache._database = dbContext;
				cache.peopleData = cache._database.People;
				cache.Refresh();
			}
			return cache;
		}

		public  PeopleModel(AppDbContext dbContext)
		{
			_database = dbContext;
			//var session = VirtualDatabase.GetDatabase(_databaseId);
			//postNames = session.postNames;
			//PeopleData = session.data;
			//Dictionary<int, Person> searchCollection = new Dictionary<int, Person>();
			peopleData = _database.People;
			_database.SaveChanges();
			
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
		public void Reset()
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
			ListEnd = false;
		}

		public void AddPerson(Person person)
		{
			_database.People.Add(person);
			_database.SaveChanges();
			Refresh();
		}

		public bool RemovePerson(int id)
		{
			var success = _database.People.Remove(_database.People.Find(id));
			_database.SaveChanges();
			Refresh();
			return success.Entity == null;
		}
	}
}
