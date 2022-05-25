using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace ASP_NET_assignments.Models
{
	public class SearchModel<T1> where T1 : IDictionary<int, Person>
	{
		readonly T1 dataSet;
		private int[] searchIndexes = {-1};
		private string lastSearch;
		T1 result;
		
		public T1 Result {
			get => result;

			private set {
				HasSearched = true;
				result = value;
			}
		}
		public bool HasSearched {
			get; private set;
		} = false;

		public SearchModel(ref T1 data, T1 searchCollection)
		{
			dataSet = data;
			result = searchCollection;
		}

		public void SetSearchScope(int[] postIndex)
		{
			this.searchIndexes = postIndex;
		}
		public void ClearSearch()
		{
			lastSearch = string.Empty;
			HasSearched = false;
			result.Clear();
		}

		/**
		 * Call if #Collection is changed (added/removeded value).
		 */
		public void RenewSearch()
		{
			if(HasSearched)
			{
				Search(lastSearch);
			}
		}

		public void Search(string value)
		{
			lastSearch = value;
			HasSearched = true;
			result.Clear();

			if(searchIndexes[0] < 0)
			{
				// Detta ger ett mystiskt cast error i runtime, men utan (T1) blir det implicit 
				// cast error från compilern och den föreslår (T1) cast som lösning (??)

				// Result = (T1)dataSet.Where(row => row.Any(post => post.Contains(value)));

				foreach(var row in dataSet)
				{
					if(row.Value.StringifyValues.Any(post => post.Contains(value)))
					{
						result.Add(row);
					}
				}
			}
			else if(searchIndexes.Length > 1)
			{
				//Result = (T1)dataSet.Where(row => 
				//	{
				//		foreach(int i in searchIndexes) { 
				//			if(row[i].Contains(value)) { return true; } }
				//		return false;
				//	});

				foreach(var row in dataSet)
				{
					foreach(int i in searchIndexes)
					{
						if(row.Value.StringifyValues[i].Contains(value))
						{
							result.Add(row);
							break;
						}
					}

				}
			}
			else
			{
				//Result = (T1)dataSet.Where(row => row[searchIndexes[0]].Contains(value));

				foreach(var row in dataSet)
				{
					if(row.Value.StringifyValues[searchIndexes[0]].Contains(value))
					{
						result.Add(row);
					}
				}
			}
		}
	}
}
