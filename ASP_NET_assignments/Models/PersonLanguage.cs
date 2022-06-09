namespace ASP_NET_assignments.Models
{
	public class PersonLanguage
	{
		public int PersonId { get; set; }
		public Person Speaker { get; set; }

		public int LanguageId { get; set; }
		public Language Language { get; set; }

	}
}
