using ASP_NET_assignments.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public abstract class I_DbDataModel<T> where T : I_DbDataModel<T>
	{
		//public abstract int Id { get; set; }
		public abstract string[] StringifyValues { get; }
		public static string[] StringifyNames { get; protected set; }
		public static string[] StringifyDisplayNames { get; protected set; }
		public static string TableName { get; protected set; }

		internal abstract void setContext(AppDbContext database);
		public abstract T MakeInstance(int id, string[] values);
	}
}
