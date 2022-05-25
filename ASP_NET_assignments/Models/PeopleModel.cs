using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ASP_NET_assignments.Models
{
	public class PeopleModel 
	{
		private static readonly Dictionary<string, PeopleModel> cache = new Dictionary<string, PeopleModel>();
		private string _databaseId;
		public string[] postNames;
		private Dictionary<int, Person> peopleData;
		private readonly Dictionary<int, Person> selectedPeopleData;

		private readonly SearchModel<Dictionary<int, Person>> searchModel;
		public Dictionary<int, Person> PeopleData
		{
			get
			{
				if(searchModel.HasSearched)
				{
					return selectedPeopleData;
				}
				return peopleData;
			}
			private set => peopleData =  value ;
		}
		private IEnumerator<KeyValuePair<int, Person>> E_people;
		private bool listEnd = false;
		public bool ListEnd
		{
			get => listEnd;
			private set => listEnd = value ;
		}
		public KeyValuePair<int, Person> GetPerson
		{
			get
			{
				if(ListEnd)
				{
					return new KeyValuePair<int, Person>(0, new Person(0, String.Empty, String.Empty, String.Empty));
				}
				KeyValuePair<int, Person> person = E_people.Current;
				ListEnd = !E_people.MoveNext();

				return person;
			}
		}
		public bool SetPerson(int id)
		{
			Reset();
			while(E_people.Current.Key != id)
			{
				if(!E_people.MoveNext())
				{
					Reset();
					return false;
				}
			}
			return true;
		}

		public static PeopleModel GetSessionModel(string sessionId)
		{
			sessionId = "dummyId";
			if(!cache.TryGetValue(sessionId, out PeopleModel model))
			{
				model = new PeopleModel(sessionId);
				cache.Add(sessionId, model);
				return model;
			}
			return model;
		}
		private PeopleModel(string databaseId)
		{
			_databaseId = databaseId;
			var session = VirtualDatabase.GetDatabase(_databaseId);
			postNames = session.postNames;
			PeopleData = session.data;
			Dictionary<int, Person> searchCollection = new Dictionary<int, Person>();
			searchModel = new SearchModel<Dictionary<int, Person>>(ref peopleData, searchCollection);
			selectedPeopleData = searchCollection;
			Reset();
		}

		public void Reset()
		{
			searchModel.ClearSearch();
			E_people = peopleData.GetEnumerator();
			ListEnd = false;
		}
		public void Search(string value)
		{
			searchModel.Search(value);
			E_people = selectedPeopleData.GetEnumerator();
			ListEnd = false;
		}

		public void AddPerson(IFormCollection formData)
		{
			Person person = new Person(
				VirtualDatabase.RandId, 
				formData["createName"],
				formData["createPhone"],
				formData["createCity"]
			);
			VirtualDatabase.AppendData(_databaseId, person);
			Reset();
		}
		public bool RemovePerson(int id)
		{
			bool success = VirtualDatabase.RemoveData(_databaseId, id);
			Reset();
			return success;
		}
	}
}
