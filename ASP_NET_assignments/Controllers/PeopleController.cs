using ASP_NET_assignments.Data;
using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_assignments.Controllers
{
	[Authorize(Roles = "User,Admin")]
	public class PeopleController : Controller
	{

		private readonly AppDbContext _dbContext;

		public PeopleController(AppDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public IActionResult Index()
		{
			return View("People", new PeopleViewModel(_dbContext));
		}

		[HttpPost]
		public IActionResult Search(string searchValue)
		{
			ViewData.Clear();
			PeopleViewModel viewModel = new PeopleViewModel(_dbContext);
			viewModel.Search(searchValue);

			return PartialView("_PeopleList", viewModel);
		}
		[HttpGet]
		public IActionResult Details(int id)
		{
			ViewData.Clear();
			PeopleViewModel viewModel = new PeopleViewModel(_dbContext);
			Person p = viewModel.GetItem(id);
			if(p == null)
			{
				var json = Json($"A person with ID {id} does not exist in the database.");
				json.StatusCode = 404;
				return json;
			}
			ViewBag.details = true;
			ViewBag.LanguageOptions = new SelectList(_dbContext.Languages, "Id", "Name");
			return PartialView("_Person", p);
		}
		[HttpPost]
		public IActionResult AddLang(int personId, int[] selectedLangs)
		{
			ViewData.Clear();
			Person p;
			JsonResult json;
			if((p = _dbContext.People.Find(personId)) != null )
			{
				var existingPL = _dbContext.PersonLanguages.Where(pl => pl.PersonId == personId);
				_dbContext.RemoveRange(existingPL.Where(pl => !selectedLangs.Contains(pl.LanguageId)).ToList());
				foreach(int langId in selectedLangs)
				{
					if(existingPL.Where(pl => pl.LanguageId == langId).Count() == 0)
					{
						_dbContext.PersonLanguages.Add(new PersonLanguage() { LanguageId = langId, PersonId = personId });
					}
				}
				_dbContext.SaveChanges();
				p.setContext(_dbContext);
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
			ViewBag.CityOptions = new SelectList(_dbContext.Cities, "Name" ,"Name");
			return PartialView("_CreatePerson");
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Create(Person person)
		{
			ViewData.Clear();
			PeopleViewModel viewModel = new PeopleViewModel(_dbContext);
			if(ModelState.IsValid)
			{
				viewModel.AddItem(person);
				ViewBag.message = "Successfully added the new person";
				return View("People", viewModel);
			}
			else
			{
				ViewBag.message = "Incorrect form data";
				return PartialView("_CreatePerson", person);
			}
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewData.Clear();
			if(_dbContext.People.Find(id) == null)
			{
				return NotFound();
			}
			else
			{
				Person person = _dbContext.People.Find(id);
				SelectList cityOptions = new SelectList(_dbContext.Cities, "Name", "Name");
				cityOptions.First(item => item.Value == person.CityName).Selected = true;
				ViewBag.CityOptions = cityOptions;
				return PartialView("_EditPerson", person);
			}
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phonenumber,CityName")] Person person)
		{
			if(id != person.Id)
				return NotFound();
			if(ModelState.IsValid)
			{
				var personToUpdate = _dbContext.People.Find(person.Id);

				if(await TryUpdateModelAsync<Person>(personToUpdate, "", p => p.Name, p => p.Phonenumber, p => p.CityName))
				{
					await _dbContext.SaveChangesAsync();
					ViewData.Clear();
					ViewBag.message = $"Successfully edited person {personToUpdate.Name}";
					return View("People");
				}
			}
			ViewData.Clear();
			ViewBag.message = "Error";
			return PartialView("_EditPerson", person);
		}

		[HttpGet]
		[HttpPost]
		public IActionResult Delete(int? id)
		{
			ViewData.Clear();
			JsonResult json;
			if (id != null)
			{
				PeopleViewModel viewModel = new PeopleViewModel(_dbContext);
				if(viewModel.RemoveItem(id.Value))
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
