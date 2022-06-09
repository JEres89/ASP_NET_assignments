using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

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
			return View("People", new PeopleViewModel(dbContext));
		}

		[HttpPost]
		public IActionResult Search(string searchValue)
		{
			ViewData.Clear();
			PeopleViewModel model = new PeopleViewModel(dbContext);
			model.Search(searchValue);

			return PartialView("_PeopleList", model);
		}
		[HttpGet]
		public IActionResult Details(int id)
		{
			ViewData.Clear();
			PeopleViewModel model = new PeopleViewModel(dbContext);
			Person p = model.GetItem(id);
			if(p == null)
			{
				var json = Json($"A person with ID {id} does not exist in the database.");
				json.StatusCode = 404;
				return json;
			}
			ViewBag.details = true;
			ViewBag.LanguageOptions = new SelectList(dbContext.Languages, "Id", "Name");
			return PartialView("_Person", p);
		}
		[HttpPost]
		public IActionResult AddLang(int personId, int[] selectedLangs)
		{
			ViewData.Clear();
			Person p;
			JsonResult json;
			if((p = dbContext.People.Find(personId)) != null )
			{
				var existingPL = dbContext.PersonLanguages.Where(pl => pl.PersonId == personId);
				dbContext.RemoveRange(existingPL.Where(pl => !selectedLangs.Contains(pl.LanguageId)).ToList());
				foreach(int langId in selectedLangs)
				{
					if(existingPL.Where(pl => pl.LanguageId == langId).Count() == 0)
					{
						dbContext.PersonLanguages.Add(new PersonLanguage() { LanguageId = langId, PersonId = personId });
						dbContext.SaveChanges();
					}
				}
				p.setContext(dbContext);
				return PartialView("_Person", p);
			}
			else
			{
				json = Json($"Person not found.");
				json.StatusCode = 404;
			}
			
			return json;
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
			PeopleViewModel model = new PeopleViewModel(dbContext);
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
				PeopleViewModel model = new PeopleViewModel(dbContext);
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
