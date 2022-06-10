using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace ASP_NET_assignments.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UsersController : Controller
	{
		private readonly AppDbContext _dbContext;
		private readonly UserManager<AppUser> _userManager;

		public UsersController(AppDbContext dbContext, UserManager<AppUser> userManager)
		{
			this._dbContext = dbContext;
			this._userManager = userManager;
		}

		public IActionResult Index()
		{
			return View("Users");
		}

		[HttpPost]
		public IActionResult GetList(string? searchValue)
		{
			ViewData.Clear();
			UserViewModel model = new UserViewModel(_dbContext, true);
			if(searchValue == null)
			{
				model.GetList();
			}
			else
			{
				model.Search(searchValue);
			}

			return PartialView("_UsersList", model);
		}
		public async Task<IActionResult> Details(string id)
		{
			ViewData.Clear();
			UserViewModel model = new UserViewModel(_dbContext, false);
			model.SetDetailsUser(id, await _userManager.GetUserAsync(User));
			return PartialView("_User", model);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(IFormCollection collection)
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

		public IActionResult Edit(int id)
		{
			ViewData.Clear();
			UserViewModel model = new UserViewModel(_dbContext, false);
			return PartialView("_User", model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, IFormCollection collection)
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
