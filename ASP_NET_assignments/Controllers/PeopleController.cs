using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ASP_NET_assignments.Controllers
{
	public class PeopleController : Controller
	{

		private readonly AppDbContext dbContext;

		public PeopleController(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public IActionResult Index()
		{
			return View("People", PeopleViewModel.GetSessionModel(dbContext));
		}

		[HttpPost]
		public IActionResult Search(string searchValue)
		{
			ViewData.Clear();
			PeopleViewModel model = PeopleViewModel.GetSessionModel(dbContext);
			model.Search(searchValue);

			return PartialView("_PeopleList", model);
		}
		[HttpGet]
		public IActionResult Details(int id)
		{
			ViewData.Clear();
			PeopleViewModel model = PeopleViewModel.GetSessionModel(dbContext);
			if(id == 0 || !model.SetNextItem(id))
			{
				var json = Json($"A person with ID {id} does not exist in the database.");
				json.StatusCode = 404;
				return json;
			}
			return PartialView("_person", model.GetItem);
		}
		public IActionResult Create()
		{
			ViewData.Clear();
			ViewBag.CityOptions = new SelectList(dbContext.Cities, "Name" ,"Name");
			return PartialView("_CreatePerson");
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Create(Person person)
		{
			ViewData.Clear();
			PeopleViewModel model = PeopleViewModel.GetSessionModel(dbContext);
			if(ModelState.IsValid)
			{
				model.AddItem(person);
				ViewBag.message = "Successfully added the new person";
				return View("People", model);
			}
			else
			{
				ViewBag.message = "Incorrect form data";
				return PartialView("_CreatePerson", person);
			}
		}
		[HttpGet]
		[HttpPost]
		public IActionResult Delete(int? id)
		{
			ViewData.Clear();
			JsonResult json;
			if (id != null)
			{
				PeopleViewModel model = PeopleViewModel.GetSessionModel(dbContext);
				if(model.RemoveItem(id.Value))
				{
					json = Json($"Person with ID {id} has been removed from the database.");
					json.StatusCode = 200;
				}
				else
				{
					json = Json($"A person with ID {id} does not exist in the database.");
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
