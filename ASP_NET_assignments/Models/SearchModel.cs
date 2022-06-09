using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public class SearchModel<T1, T2> where T1 : DbSet<T2> where T2 : I_DbDataModel<T2>
	{
		public T1 dataSet;
		private int[] searchColumns = new int[0];
		private string lastSearch;
		List<T2> result = new List<T2>();
		
		public List<T2> Result {
			get => result;
		}
		public bool HasSearched {
			get; private set;
		} = false;

		public SearchModel(T1 data)
		{
			dataSet = data;
		}
		public void SetSearchScope(int[] columnIndexes)
		{
			this.searchColumns = columnIndexes;
		}
		public void ClearSearch()
		{
			lastSearch = string.Empty;
			HasSearched = false;
			result.Clear();
		}

		public bool RenewSearch()
		{
			return Search(lastSearch);
		}

		public bool Search(string value)
		{
			result.Clear();

			if(value == null)
			{
				HasSearched = false;
				return false;
			}

			lastSearch = value;
			HasSearched = true;
			value = $"%{value}%";
			if(searchColumns.Length < 1)
			{
				string[] columnNames = I_DbDataModel<T2>.StringifyNames;
				string filters = string.Empty;
				//SqlParameter[] sqlParameters = new SqlParameter[columnNames.Length];
				SqlParameter sqlParameter = new SqlParameter("value", value);
				for(int i = 0 ; i < columnNames.Length ; i++)
				{
					string columnName = columnNames[i];

					if(filters != string.Empty)
					{
						filters += " OR ";
					}
					filters += $"{columnName} LIKE @value";
					//sqlParameters[i] = new SqlParameter(columnName, value);
				}
				//string filters = "@value in (";
				//for(int i = 0 ; i < columnNames.Length ; i++)
				//{
				//	string columnName = columnNames[i];
				//	char divider = i == columnNames.Length - 1 ? ')' : '\u002C';
				//	filters += $"{columnName}{divider} ";
				//}
				//result.AddRange(dataSet.FromSqlRaw(query, sqlParameter));

				string query = $"SELECT * FROM dbo.{I_DbDataModel<T2>.TableName} WHERE {filters}";

				result.AddRange(dataSet.FromSqlRaw(query, sqlParameter));

				//result = I_DbDataModel<T2>.Search(dataSet, value).ToList();
				//result = dataSet.Where(row => row.StringifyValues.Any(col => col.Contains(value))).ToList();
			}
			else if(searchColumns.Length > 1)
			{
				string[] columnNames = I_DbDataModel<T2>.StringifyNames;
				string filters = string.Empty;
				SqlParameter sqlParameter = new SqlParameter("value", value);

				for(int i = 0 ; i < searchColumns.Length ; i++)
				{
					int column = searchColumns[i];
					string columnName = columnNames[column];

					if(filters != string.Empty)
					{
						filters += " OR ";
					}
					filters += $"{columnName} LIKE '%@value%'";
				}

				string query = $"SELECT * FROM {nameof(T2)} WHERE {filters}";

				result = dataSet.FromSqlRaw(query, sqlParameter).ToList();

			}
			//else
			//{
			//	foreach(var row in dataSet)
			//	{
			//		if(row.StringifyValues[searchColumns[0]].Contains(value))
			//		{
			//			result.Add(row);
			//		}
			//	}
			//}
			return true;
		}
	}
}
