using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_NET_assignments.Controllers
{
	public class CountriesController : Controller
	{
		private readonly AppDbContext dbContext;

		public CountriesController(AppDbContext context)
		{
			dbContext = context;
		}

		public IActionResult Index()
		{
			return View("Countries", new CountryViewModel(dbContext));
		}

		[HttpPost]
		public IActionResult Search(string searchValue)
		{
			ViewData.Clear();
			CountryViewModel model = new CountryViewModel(dbContext);
			model.Search(searchValue);

			return PartialView("_CountriesList", model);
		}
		[HttpGet]
		public IActionResult Details(int id)
		{
			ViewData.Clear();
			CountryViewModel model = new CountryViewModel(dbContext);
			Country c = model.GetItem(id);
			if(c == null)
			{
				var json = Json($"A Country with ID {id} does not exist in the database.");
				json.StatusCode = 404;
				return json;
			}
			return PartialView("_Country", c);
		}
		public IActionResult Create()
		{
			ViewData.Clear();
			ViewBag.CityOptions = new SelectList(dbContext.Cities, "Id", "Name");
			return PartialView("_CreateCountry");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Country country, int[] cities)
		{
			ViewData.Clear();
			CountryViewModel model = new CountryViewModel(dbContext);
			if(ModelState.IsValid)
			{
				foreach(int cityId in cities)
				{
					country.Cities.Add(dbContext.Cities.Find(cityId));
				}
				model.AddItem(country);
				ViewBag.message = "Successfully added the new country";
				return View("Countries", model);
			}
			else
			{
				ViewBag.message = "Incorrect form data";
				return PartialView("_CreateCountry", country);
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
				CountryViewModel model = new CountryViewModel(dbContext);
				if(model.RemoveItem(id.Value))
				{
					json = Json($"Country with ID {id} has been removed from the database.");
					json.StatusCode = 200;
				}
				else
				{
					json = Json($"A country with ID {id} does not exist in the database.");
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
