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
			return View("People", new PeopleModel(dbContext));
		}

		[HttpPost]
		public IActionResult Search(string searchValue)
		{
			PeopleModel model = new PeopleModel(dbContext);
			model.Search(searchValue);

			return PartialView("_PeopleList", model);
		}
		[HttpGet]
		public IActionResult Details(int id)
		{
			PeopleModel model = new PeopleModel(dbContext);
			if(id == 0 || !model.SetNextItem(id))
			{
				var json = Json($"<p>A person with ID {id} does not exist in the database.</p>");
				json.StatusCode = 404;
				return json;
			}
			return PartialView("_person", model.GetItem);
		}
		public IActionResult Create()
		{
			ViewBag.CityOptions = new SelectList(dbContext.Cities, "Name" ,"Name");
			return PartialView("_CreatePerson");
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Create(Person person)
		{
			PeopleModel model = new PeopleModel(dbContext);
			if(ModelState.IsValid)
			{
				model.AddItem(person);
				ViewBag.addPersonResult = "<p>Successfully added the new person</p>";
				return View("People", model);
			}
			else
			{
				ViewBag.addPersonResult = "<p>Incorrect form data</p>";
				return PartialView("_CreatePerson", person);
			}
			//return View("People", model);
		}
		[HttpGet]
		[HttpPost]
		public IActionResult Delete(int? id)
		{
			JsonResult json;
			if (id != null)
			{
				PeopleModel model = new PeopleModel(dbContext);
				if(model.RemoveItem(id.Value))
				{
					json = Json($"<p>Person with ID {id} has been removed from the database.</p>");
					json.StatusCode = 200;
				}
				else
				{
					json = Json($"<p>A person with ID {id} does not exist in the database.</p>");
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
