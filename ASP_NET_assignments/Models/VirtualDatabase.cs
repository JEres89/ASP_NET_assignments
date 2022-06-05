using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public static class VirtualDatabase
	{
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

		public static List<T> GetSeedData<T>(T type) where T : I_DbDataModel<T>
		{
			List<T> list = new List<T>();
			switch(type)
			{
				case Person _:
					list.AddRange( new T[] {
						type.MakeInstance(new string[]{"Jens Eresund", "+46706845909", "Göteborg" }),
						type.MakeInstance(new string[]{"Abel Abrahamsson", "+00123456789", "Staden" }),
						type.MakeInstance(new string[]{"Bror Björn", "+5555555555", "Skogen" }),
						type.MakeInstance(new string[]{"Örjan Örn", "1111111111", "Luftslottet" })
					});
					break;
				case City _:
					list.AddRange( new T[] {
						type.MakeInstance(new string[]{"Göteborg" }),
						type.MakeInstance(new string[]{"Staden" }),
						type.MakeInstance(new string[]{"Skogen" }),
						type.MakeInstance(new string[]{"Luftslottet" })
					});
					break;
				case "Country":

					break;
				default:
					break;
			}
			
			return list;
		}
	}
}
