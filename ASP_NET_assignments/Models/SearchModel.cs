using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	/**
	 * TODO:
	 * Replace IList<string> (string[] in PeopleModel) with List< key : string > 
	 * and int[] searchIndexes with List<key> searchKeys
	 * to accomodate variable length database rows.
	 */
	public class SearchModel<T1, T2> where T1 : ICollection<T2> where T2 : IList<string>
	{
		readonly T1 dataSet;
		private int[] searchIndexes = {-1};
		private string lastSearch;
		T1 result;
		public T1 Result
		{
			get => result;

			private set
			{
				HasSearched = true;
				result = value;
			}
		}
		public bool HasSearched
		{
			get; private set;
		} = false;

		// <searchValue, 
		//Func<string, string, bool> GetValue;

		public SearchModel(ref T1 data, T1 searchCollection)//, Func<string, string, bool> getValue)
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

				foreach(T2 row in dataSet)
				{
					if(row.Any(post => post.Contains(value)))
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

				foreach(T2 row in dataSet)
				{
					foreach(int i in searchIndexes)
					{
						if(row[i].Contains(value))
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

				foreach(T2 row in dataSet)
				{
					if(row[searchIndexes[0]].Contains(value))
					{
						result.Add(row);
					}
				}
			}
		}
	}
}
