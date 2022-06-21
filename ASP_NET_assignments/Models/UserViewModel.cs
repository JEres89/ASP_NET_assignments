using ASP_NET_assignments.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_NET_assignments.Models
{
	public class UserViewModel
	{
		private AppDbContext dbContext;

		private IAsyncEnumerator<AppUser> E_users;
		public bool ListEnd { get; private set; } = false;
		public bool UseListMode { get; private set; }
		public bool NextUser {
			get {
				if(!UseListMode || ( ListEnd = !E_users.MoveNextAsync().Result ))
					return false;
				CurrentUser = E_users.Current;
				return true;
			}
		}
		public AppUser CurrentUser { get; private set; }
		private PropertyInfo[] props;
		private BitArray showCols;

		private static List<string> listColumns { get; } = new List<string>{
			"First name", "Last name", "Username", "Email"
		};
		public List<PropertyInfo> detailsColumns{ get; private set; }
		public List<string> ColumnNames { 
			get{
				if(UseListMode)
				{
					return listColumns;
				}
				return detailsColumns.ConvertAll(I => I.Name);
			} 
		}

		private List<string> columnValues;
		public List<string> ColumnValues {
			get {
				if(UseListMode)
				{
					return new List<string> {
						CurrentUser.FirstName, CurrentUser.LastName, CurrentUser.UserName, CurrentUser.Email 
					};
				}
				return columnValues;
			}
		}

		public UserViewModel(AppDbContext dbContext, string listFilter)
		{
			this.dbContext = dbContext;
			UseListMode = true;
			GenerateList(listFilter);
		}
		public UserViewModel(AppDbContext dbContext, AppUser currentAdmin, string detailsOfID)
		{
			this.dbContext = dbContext;
			UseListMode = false;
			SetDetailsUser(detailsOfID);
		}

		public void SetDetailsUser(string id)
		{
			CurrentUser = dbContext.Users.FirstOrDefault(u => u.Id == id);

			showCols = new BitArray(new[] { int.MaxValue });
			props = typeof(AppUser).GetProperties();
			detailsColumns = new List<PropertyInfo>();
			columnValues = new List<string>();

			for(int i = 0 ; i < props.Length ; i++)
			{
				if(showCols[i])
				{
					detailsColumns.Add(props[i]);
					var value = props[i].GetValue(CurrentUser);

					columnValues.Add(value == null ? "null" : value.ToString());
				}
			}
		}
		public List<SelectListItem> RolesOptions()
		{
			List<IdentityUserRole<string>> existingRoles = dbContext.UserRoles.Where(role => role.UserId == CurrentUser.Id).ToList();

			List<SelectListItem> roleItems = dbContext.Roles.ToList().ConvertAll(
				role => new SelectListItem(
					role.Name,
					role.Id,
					existingRoles.Any(
						currentRole => currentRole.RoleId == role.Id
					)
				));
			//if(adminUser == CurrentUser)
			//{
			//	roleItems.Remove(roleItems.Find(role => role.Text == "Admin"));
			//}

			return roleItems;

			//.Join(CurrentUser.IdentityUserRoles, role => role.Id, userRole => userRole.RoleId, (role, userRole) => role));
		}

		private void GenerateList(string filter)
		{
			if(filter != null)
				Search(filter);
			else
				E_users = dbContext.Users.AsAsyncEnumerable().GetAsyncEnumerator();
		}
		internal void Search(string searchValue)
		{
			E_users = dbContext.Users.Where(u => u.FirstName.Contains(searchValue) || u.LastName.Contains(searchValue) || u.Email.Contains(searchValue)).AsAsyncEnumerable().GetAsyncEnumerator();
		}

		public void AddItem(AppUser user)
		{
			dbContext.Users.Add(user);
			//dbContext.SaveChanges();
		}

		public bool RemoveItem(string id)
		{
			var user = dbContext.Users.Find(id);
			if(user == null)
			{
				return false;
			}
			var result = dbContext.Users.Remove(user);
			//dbContext.SaveChanges();
			return result.State	== EntityState.Deleted;
		}
	}
}
