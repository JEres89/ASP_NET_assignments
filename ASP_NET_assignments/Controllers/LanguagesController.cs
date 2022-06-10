using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_NET_assignments.Controllers
{
	[Authorize(Roles = "Admin")]
	public class LanguagesController : Controller
	{
		private readonly AppDbContext dbContext;

		public LanguagesController(AppDbContext context)
		{
			dbContext = context;
		}

		public IActionResult Index()
		{
			return View("Languages", new LanguageViewModel(dbContext));
		}

		[HttpPost]
		public IActionResult Search(string searchValue)
		{
			ViewData.Clear();
			LanguageViewModel model = new LanguageViewModel(dbContext);
			model.Search(searchValue);

			return PartialView("_LanguagesList", model);
		}
		[HttpGet]
		public IActionResult Details(int id)
		{
			ViewData.Clear();
			LanguageViewModel model = new LanguageViewModel(dbContext);
			Language l = model.GetItem(id);
			if(l == null)
			{
				var json = Json($"A Language with ID {id} does not exist in the database.");
				json.StatusCode = 404;
				return json;
			}
			return PartialView("_Language", l);
		}
		public IActionResult Create()
		{
			ViewData.Clear();
			ViewBag.PersonOptions = new SelectList(dbContext.People, "Name", "Name");
			return PartialView("_CreateLanguage");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Language language)
		{
			ViewData.Clear();
			LanguageViewModel model = new LanguageViewModel(dbContext);
			if(ModelState.IsValid)
			{
				model.AddItem(language);
				ViewBag.message = "Successfully added the new language";
				return View("Languages", model);
			}
			else
			{
				ViewBag.message = "Incorrect form data";
				return PartialView("_CreateLanguage", language);
			}
		}

		[HttpGet]
		[HttpPost]
		public IActionResult Delete(int? id)
		{
			ViewData.Clear();
			JsonResult json;
			if(id != null)
			{
				LanguageViewModel model = new LanguageViewModel(dbContext);
				if(model.RemoveItem(id.Value))
				{
					json = Json($"Language with ID {id} has been removed from the database.");
					json.StatusCode = 200;
				}
				else
				{
					json = Json($"A language with ID {id} does not exist in the database.");
					json.StatusCode = 404;
				}
			}
			else
			{
				json = new JsonResult(null);
			}
			return json;
		}
	}
}
