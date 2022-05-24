using System;
using System.Collections.Generic;

namespace ASP_NET_assignments.Models
{
	public static class VirtualDatabase
	{
		private static Dictionary<string, (string[] postNames, Dictionary<int, string[]> data)> _virtualDatabases = new Dictionary<string, (string[], Dictionary<int, string[]>)>();
		private static readonly Random rand = new Random();
		public static int RandId
		{
			get
			{
				int nextRand = rand.Next();
				while(usedIds.Contains(nextRand))
				{
					nextRand = rand.Next();
				}
				usedIds.Add(nextRand);
				return nextRand;
			}
		}
		private static List<int> usedIds = new List<int>();
		public static (string[] postNames, Dictionary<int, string[]> data) GetDatabase(string databaseId)
		{
			databaseId = "dummyId";
			if(!_virtualDatabases.TryGetValue(databaseId, out var list))
			{
				list.postNames = new string[] { "Name", "Phonenumber", "City of Residence" };
				list.data = new Dictionary<int, string[]>
				(
					new KeyValuePair<int, string[]>[]
					{
						new KeyValuePair<int, string[]>( RandId, new string[] { "Jens Eresund", "+46706845909", "Göteborg"}),
						new KeyValuePair<int, string[]>( RandId, new string[] { "Abel Abrahamsson", "+00123456789", "Staden"}),
						new KeyValuePair<int, string[]>( RandId, new string[] { "Bror Björn", "+5555555555", "Skogen"}),
						new KeyValuePair<int, string[]>( RandId, new string[] { "Örjan Örn", "1111111111", "Luftslottet"})
					}
				);
				_virtualDatabases.Add(databaseId, list);
			}

			return list;
		}

		public static bool AppendData(string _databaseId, string[] data)
		{
			_databaseId = "dummyId";
			if(_virtualDatabases.TryGetValue(_databaseId, out var list))
			{
				list.data.Add(RandId, data);
				//_virtualDatabases[_databaseId] = list;
				return true;
			}

			return false;
		}
		public static bool RemoveData(string _databaseId, int rowId)
		{
			if(_virtualDatabases.TryGetValue(_databaseId, out var list))
			{
				return list.data.Remove(rowId);
			}
			return false;
		}
	}
}
