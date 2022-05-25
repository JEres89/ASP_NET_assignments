using System;
using System.Collections.Generic;

namespace ASP_NET_assignments.Models
{
	public static class VirtualDatabase
	{
		private static Dictionary<string, (string[] postNames, Dictionary<int, Person> data)> _virtualDatabases = new Dictionary<string, (string[], Dictionary<int, Person>)>();
		private static readonly Random rand = new Random();

		private static int randId;
		public static int RandId
		{
			get
			{
				randId = rand.Next();
				while(usedIds.Contains(randId))
				{
					randId = rand.Next();
				}
				usedIds.Add(randId);
				return randId;
			}
		}
		private static List<int> usedIds = new List<int>();
		public static (string[] postNames, Dictionary<int, Person> data) GetDatabase(string databaseId)
		{
			databaseId = "dummyId";
			if(!_virtualDatabases.TryGetValue(databaseId, out var list))
			{
				list.postNames = new string[] { "Name", "Phonenumber", "City of Residence" };
				list.data = new Dictionary<int, Person>
				(
					new KeyValuePair<int, Person>[]
					{
						new KeyValuePair<int, Person>( RandId, new Person(randId, "Jens Eresund", "+46706845909", "Göteborg")),
						new KeyValuePair<int, Person>( RandId, new Person(randId, "Abel Abrahamsson", "+00123456789", "Staden")),
						new KeyValuePair<int, Person>( RandId, new Person(randId, "Bror Björn", "+5555555555", "Skogen")),
						new KeyValuePair<int, Person>( RandId, new Person(randId, "Örjan Örn", "1111111111", "Luftslottet"))
					}
				);
				_virtualDatabases.Add(databaseId, list);
			}

			return list;
		}

		public static bool AppendData(string _databaseId, Person person)
		{
			_databaseId = "dummyId";
			if(_virtualDatabases.TryGetValue(_databaseId, out var list))
			{
				list.data.Add(person.Id, person);
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
