using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public abstract class I_DbDataModel<T> where T : I_DbDataModel<T>
	{
		public abstract int Id { get; set; }
		public abstract string[] StringifyValues { get; }
		public static string[] StringifyNames { get; }
		public static string[] StringifyDisplayNames { get; }

		public abstract T MakeInstance(string[] values);
	}
}
