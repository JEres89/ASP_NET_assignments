using System;
using System.Collections.Generic;

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

		public static List<PersonLanguage> GetPLSeedData(List<Person> people, List<Language> languages)
		{
			List<PersonLanguage> plList = new List<PersonLanguage>();
			foreach(Person person in people)
			{
				PersonLanguage plLanguage = new PersonLanguage() {
					 PersonId = person.Id,
					 LanguageId = languages[1].Id
				};
				plList.Add(plLanguage);

				int i = rand.Next(2) == 0 ? -1 : 1;
				plLanguage = new PersonLanguage() {
					
					PersonId = person.Id,
					
					LanguageId = languages[1+i].Id
				};
				plList.Add(plLanguage);
			}

			return plList;
		}
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
						type.MakeInstance(new string[]{"Göteborg","Sverige"}),
						type.MakeInstance(new string[]{"Staden","Sverige" }),
						type.MakeInstance(new string[]{"Skogen","Norge" }),
						type.MakeInstance(new string[]{"Luftslottet","Ingenstans" })
					});
					break;
				case Country _:
					list.AddRange(new T[] {
						type.MakeInstance(new string[]{"Sverige" }),
						type.MakeInstance(new string[]{"Norge" }),
						type.MakeInstance(new string[]{"Ingenstans" })
					});
					break;
				case Language _:
					list.AddRange(new T[] {
						type.MakeInstance(new string[]{"Svenska"}),
						type.MakeInstance(new string[]{"Engelska"}),
						type.MakeInstance(new string[]{"Spanska"}),
					});
					break;
				default:
					break;
			}
			
			return list;
		}
	}
}
