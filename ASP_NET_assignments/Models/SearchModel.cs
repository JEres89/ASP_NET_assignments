using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace ASP_NET_assignments.Models
{
	public class SearchModel<T1, T2> where T1 : DbSet<T2> where T2 : class, I_DBformData
	{
		readonly T1 dataSet;
		private int[] searchPosts = new int[0];
		private string lastSearch;
		List<T2> result = new List<T2>();
		
		public List<T2> Result {
			get => result;
		}
		public bool HasSearched {
			get; private set;
		} = false;

		public SearchModel(ref T1 data)
		{
			dataSet = data;
		}

		public void SetSearchScope(int[] postIndexes)
		{
			this.searchPosts = postIndexes;
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

			if(searchPosts.Length < 1)
			{
				result = dataSet.ToList().Where(row => row.StringifyValues.Any(post => post.Contains(value))).ToList();
			}
			else if(searchPosts.Length > 1)
			{
				foreach(var row in dataSet)
				{
					foreach(int p in searchPosts)
					{
						if(row.StringifyValues[p].Contains(value))
						{ result.Add(row); break; }
					}
				}
			}
			else
			{
				foreach(var row in dataSet)
				{
					if(row.StringifyValues[searchPosts[0]].Contains(value))
					{
						result.Add(row);
					}
				}
			}
			return true;
		}
	}
}
