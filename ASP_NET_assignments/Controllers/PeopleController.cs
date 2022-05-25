using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Net.Http;
using System.Web;

namespace ASP_NET_assignments.Controllers
{
	public class PeopleController : Controller
	{

		public IActionResult Index()
		{
			return View("People", PeopleModel.GetSessionModel(HttpContext.Session.Id));
		}

		[HttpPost]
		public IActionResult FormSubmit(IFormCollection formData)
		{
			if(formData.TryGetValue("formName", out StringValues target))
			{
				switch(target[0])
				{
					case "searchPerson":
						return Search(formData);
					case "createPerson":
						return Create(formData);
					default:
						break;
				}
			}

			return null;
		}

		public IActionResult Search(IFormCollection formData)
		{
			PeopleModel model = PeopleModel.GetSessionModel(HttpContext.Session.Id);
			model.Search(formData["searchValue"][0]);

			return PartialView("_PeopleList", model);
		}
		[HttpGet]
		public IActionResult Details(int id)
		{
			PeopleModel model = PeopleModel.GetSessionModel(HttpContext.Session.Id);
			if(id == 0 || !model.SetPerson(id))
			{
				var json = Json($"<p>A person with ID {id} does not exist in the database.</p>");
				json.StatusCode = 404;
				return json;
			}
			return PartialView("_person", model.GetPerson.Value);
		}

		public IActionResult Create(IFormCollection formData)
		{
			PeopleModel model = PeopleModel.GetSessionModel(HttpContext.Session.Id);
			model.AddPerson(formData);

			return PartialView("_PeopleList", model);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			PeopleModel model = PeopleModel.GetSessionModel(HttpContext.Session.Id);
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
