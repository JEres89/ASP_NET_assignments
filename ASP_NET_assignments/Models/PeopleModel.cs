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
		private Dictionary<int, string[]> peopleData;
		private readonly Dictionary<int, string[]> selectedPeopleData;

		private readonly SearchModel<Dictionary<int, string[]>, string[]> searchModel;
		public Dictionary<int, string[]> PeopleData
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
		private IEnumerator<KeyValuePair<int, string[]>> E_people;
		private bool listEnd = false;
		public bool ListEnd
		{
			get => listEnd;
			private set => listEnd = value ;
		}
		public KeyValuePair<int, string[]> GetPerson
		{
			get
			{
				if(ListEnd)
				{
					return new KeyValuePair<int, string[]>();
				}
				KeyValuePair<int, string[]> person = E_people.Current;
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
			Dictionary<int, string[]> searchCollection = new Dictionary<int, string[]>();
			searchModel = new SearchModel<Dictionary<int, string[]>, string[]>(ref peopleData, searchCollection);
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
			string[] data =
			{
				formData["createName"],
				formData["createPhone"],
				formData["createCity"]
			};
			VirtualDatabase.AppendData(_databaseId, data);
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
