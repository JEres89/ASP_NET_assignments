using System.Collections.Generic;

namespace ASP_NET_assignments.Models
{
	public static class VirtualDatabase
	{
		private static Dictionary<string, (string[] postNames, List<string[]> data)> _virtualDatabases = new Dictionary<string, (string[], List<string[]>)>();

		public static (string[] postNames, List<string[]> data) GetSessionData(string sessionId)
		{
			if(!_virtualDatabases.TryGetValue(sessionId, out var list))
			{
				list.postNames = new string[] { "Name", "Phonenumber", "City of Residence" };
				list.data = new List<string[]>(new string[][]
				{
				new string[] {"Jens Eresund", "+46706845909", "Göteborg"},
				new string[] {"Abel Abrahamsson", "+00123456789", "Staden"},
				new string[] {"Bror Björn", "+5555555555", "Skogen"},
				new string[] {"Örjan Örn", "1111111111", "Luftslottet"}
				});
				_virtualDatabases.Add(sessionId, list);
			}

			return list;
		}

		public static bool AppendData(string sessionId, string[] data)
		{
			if(_virtualDatabases.TryGetValue(sessionId, out var list))
			{
				list.data.Add(data);
				_virtualDatabases[sessionId] = list;
				return true;
			}

			return false;
		}
		public static bool RemoveData(string sessionId, string[] data)
		{
			if(_virtualDatabases.TryGetValue(sessionId, out var list))
			{
				list.data.Add(data);
				return true;
			}

			return false;
		}
	}

}
