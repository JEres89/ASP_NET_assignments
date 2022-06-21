using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASP_NET_assignments.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UsersController : Controller
	{
		private readonly AppDbContext _dbContext;
		private readonly UserManager<AppUser> _userManager;

		public UsersController(AppDbContext dbContext, UserManager<AppUser> userManager)
		{
			_dbContext = dbContext;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			return View("Users");
		}

		[HttpPost]
		public IActionResult GetList(string searchValue)
		{
			ViewData.Clear();
			UserViewModel model = new UserViewModel(_dbContext, searchValue);

			return PartialView("_UsersList", model);
		}

		/**
		 * 
		 */
		[HttpGet]
		public async Task<IActionResult> DetailsAsync(string id)
		{
			ViewData.Clear();
			if(_dbContext.Users.Find(id)==null)
			{
				return NotFound();
			}
			else
			{
				UserViewModel viewModel = new UserViewModel(_dbContext, await _userManager.GetUserAsync(User), id);
				return PartialView("_User", viewModel);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			ViewData.Clear();
			if(_dbContext.Users.Find(id) == null)
			{
				return NotFound();
			}
			else
			{
				UserViewModel viewModel = new UserViewModel(_dbContext, await _userManager.GetUserAsync(User), id);

				ViewBag.RoleOptions = viewModel.RolesOptions();
				return PartialView("_EditUser", viewModel.CurrentUser);
			}
		}
		[HttpPost]
		public async Task<IActionResult> Edit(string id, [Bind] AppUser user)
		{
			if(id != user.Id)
				return NotFound();
			if(ModelState.IsValid)
			{
				var userToUpdate = _dbContext.Users.Find(id);

				if(await TryUpdateModelAsync<AppUser>(userToUpdate, "", u => u.FirstName, u => u.LastName, u=>u.Birthdate, u => u.Email ))
				{
					var selectedRoles = new List<string>();
					foreach(var value in HttpContext.Request.Form.ToList())
					{
						if(value.Key.StartsWith("role:"))
						{
							selectedRoles.Add(value.Key[5..]);
						}
					}
					var extraRoles = _dbContext.UserRoles.Where(ur => ur.UserId == id).Where(ur => !selectedRoles.Contains(ur.RoleId));
					_dbContext.UserRoles.RemoveRange(extraRoles);
					foreach(string roleId in selectedRoles)
					{
						if(_dbContext.UserRoles.Find(id, roleId) == null)
						{
							_dbContext.UserRoles.Add(new IdentityUserRole<string>() {
								RoleId = roleId,
								UserId = id
							});
						}
					}
					await _dbContext.SaveChangesAsync();
					ViewData.Clear();
					ViewBag.message = $"Successfully edited user {userToUpdate.UserName}";
					return View("Users");
				}
			}
			ViewData.Clear();
			ViewBag.message = "Error";
			return PartialView("_EditUser", user);
		}

		public IActionResult Delete(int id)
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
