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
    public class CitiesController : Controller
    {
		private readonly AppDbContext dbContext;

        public CitiesController(AppDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View("Cities", new CityModel(dbContext));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            CityModel model = new CityModel(dbContext);
            if(id == 0 || !model.SetNextItem(id))
            {
                var json = Json($"<p>A City with ID {id} does not exist in the database.</p>");
                json.StatusCode = 404;
                return json;
            }
            return PartialView("_City", model.GetItem);
        }
        public IActionResult Create()
        {
            ViewBag.PersonOptions = new SelectList(dbContext.People, "Name" , "Name");
            return PartialView("_CreateCity");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City city)
        {
            CityModel model = new CityModel(dbContext);
            if (ModelState.IsValid)
            {
                model.AddItem(city);
                ViewBag.addCityResult = "<p>Successfully added the new city</p>";
                return View("Cities", model);
            }
            else
            {
                ViewBag.addCityResult = "<p>Incorrect form data</p>";
                return PartialView("_CreateCity", city);
            }
            //return View(city);
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var city = await dbContext.Cities.FindAsync(id);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(city);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] City city)
        //{
        //    if (id != city.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            dbContext.Update(city);
        //            await dbContext.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CityExists(city.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(city);
        //}
        [HttpGet]
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            JsonResult json;
            if(id != null)
            {
                CityModel model = new CityModel(dbContext);
                if(model.RemoveItem(id.Value))
                {
                    json = Json($"<p>City with ID {id} has been removed from the database.</p>");
                    json.StatusCode = 200;
                }
                else
                {
                    json = Json($"<p>A city with ID {id} does not exist in the database.</p>");
                    json.StatusCode = 404;
                }
			}
			else
			{
                json = new JsonResult(null);
            }
            return json;
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var city = await dbContext.Cities.FindAsync(id);
        //    dbContext.Cities.Remove(city);
        //    await dbContext.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CityExists(int id)
        //{
        //    return dbContext.Cities.Any(e => e.Id == id);
        //}
    }
}
