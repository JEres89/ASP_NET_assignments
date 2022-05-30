using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
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
			if(id == 0 || !model.SetPerson(id))
			{
				var json = Json($"<p>A person with ID {id} does not exist in the database.</p>");
				json.StatusCode = 404;
				return json;
			}
			return PartialView("_person", model.GetPerson);
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Create(Person person)
		{
			PeopleModel model = new PeopleModel(dbContext);
			if(ModelState.IsValid)
			{
				model.AddPerson(person);
				ViewBag.addPersonResult = "<p>Successfully added the new person</p>";
			}
			else
			{
				ViewBag.addPersonResult = "<p>Incorrect form data</p>";
			}
			return View("People", model);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return PartialView("CreatePersonView");
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			PeopleModel model = new PeopleModel(dbContext);
			if(model.RemovePerson(id))
			{
				var json = Json($"<p>Person with ID {id} has been removed from the database.</p>");
				json.StatusCode = 200;
				return json;
			}
			else
			{
				var json = Json($"<p>A person with ID {id} does not exist in the database.</p>");
				json.StatusCode = 404;
				return json;
			}
			
		}

	}
}
