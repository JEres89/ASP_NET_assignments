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
		public IActionResult Details(int id)
		{

			return View();
		}


		public IActionResult Create(IFormCollection formData)
		{
			PeopleModel model = PeopleModel.GetSessionModel(HttpContext.Session.Id);
			model.AddPerson(formData);

			return PartialView("_PeopleList", model);
		}

		public IActionResult Edit(int id)
		{
			return View();
		}

		public IActionResult Delete(int id)
		{
			PeopleModel model = PeopleModel.GetSessionModel(HttpContext.Session.Id);
			model.RemovePerson(id);

			return PartialView("_PeopleList", model);
		}

	}
}
