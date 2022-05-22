using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ASP_NET_assignments.Models
{
	public class PeopleModel 
	{
		private static readonly Dictionary<string, PeopleModel> cache = new Dictionary<string, PeopleModel>();
		private string _sessionId;
		public string[] postNames;
		private List<string[]> peopleData;
		private readonly List<string[]> selectedPeopleData;

		private readonly SearchModel<List<string[]>, string[]> searchModel;
		public List<string[]> PeopleData
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

		public static PeopleModel GetSessionModel(string sessionId)
		{
			if(!cache.TryGetValue(sessionId, out PeopleModel model))
			{
				model = new PeopleModel(sessionId);
				cache.Add(sessionId, model);
				return model;
			}
			return model;
		}
		private PeopleModel(string sessionId)
		{
			_sessionId = sessionId;
			var session = VirtualDatabase.GetSessionData(sessionId);
			postNames = session.postNames;
			PeopleData = session.data;
			List<string[]> searchCollection = new List<string[]>();
			searchModel = new SearchModel<List<string[]>, string[]>(ref peopleData, searchCollection);
			selectedPeopleData = searchCollection;

		}
		//public PeopleModel(string sessionId) : this(
		//	sessionId,
		//	new List<string[]>(new string[][]
		//	{
		//		new string[] {"Jens Eresund", "+46706845909", "Göteborg"},
		//		new string[] {"Abel Abrahamsson", "+00123456789", "Staden"},
		//		new string[] {"Bror Björn", "+5555555555", "Skogen"},
		//		new string[] {"Örjan Örn", "1111111111", "Luftslottet"}
		//	}),
		//	new string[]
		//	{
		//		"Name", "Phonenumber", "City of Residence"
		//	})
		//{
		//}
		public void Search(string value)
		{
			searchModel.Search(value);
		}

		public void AddPerson(IFormCollection formData)
		{
			string[] data =
			{
				formData["createName"],
				formData["createPhone"],
				formData["createCity"]
			};
			peopleData.Add(data);
			searchModel.RenewSearch();
		}
		public void RemovePerson(int index)
		{
			peopleData.RemoveAt(index);
			searchModel.RenewSearch();
		}
	}
}
