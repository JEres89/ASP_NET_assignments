using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_NET_assignments.Controllers
{
	[Authorize(Roles = "Admin")]
	public class CitiesController : Controller
	{
		private readonly AppDbContext dbContext;

		public CitiesController(AppDbContext context)
		{
			dbContext = context;
		}

		public IActionResult Index()
		{
			return View("Cities", new CityViewModel(dbContext));
		}

		[HttpPost]
		public IActionResult Search(string searchValue)
		{
			ViewData.Clear();
			CityViewModel model = new CityViewModel(dbContext);
			model.Search(searchValue);

			return PartialView("_CitiesList", model);
		}
		[HttpGet]
		public IActionResult Details(int id)
		{
			ViewData.Clear();
			CityViewModel model = new CityViewModel(dbContext);
			City c = model.GetItem(id);
			if(c == null)
			{
				var json = Json($"A City with ID {id} does not exist in the database.");
				json.StatusCode = 404;
				return json;
			}
			return PartialView("_City", c);
		}
		public IActionResult Create()
		{
			ViewData.Clear();
			ViewBag.PersonOptions = new SelectList(dbContext.People, "Id", "Name");
			ViewBag.CountryOptions = new SelectList(dbContext.Countries, "Name", "Name");
			return PartialView("_CreateCity");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(City city, int[] people)
		{
			ViewData.Clear();
			CityViewModel model = new CityViewModel(dbContext);
			if(ModelState.IsValid)
			{
				foreach(int personId in people)
				{
					city.People.Add(dbContext.People.Find(personId));
				}
				model.AddItem(city);

				ViewBag.message = "Successfully added the new city";
				
				return View("Cities", model);
			}
			else
			{
				ViewBag.message = "Incorrect form data";
				return PartialView("_CreateCity", city);
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
				CityViewModel model = new CityViewModel(dbContext);
				if(model.RemoveItem(id.Value))
				{
					json = Json($"City with ID {id} has been removed from the database.");
					json.StatusCode = 200;
				}
				else
				{
					json = Json($"A city with ID {id} does not exist in the database.");
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
