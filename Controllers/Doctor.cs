using ASP_NET_assignments.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_assignments.Controllers
{
	public class Doctor : Controller
	{
		public IActionResult FeverCheck()
		{
			return View();
		}

		[HttpPost]
		public IActionResult FeverCheck(char scale, float temp)
		{

			ViewBag.message = DoctorModel.TempCheck(scale, temp);
			
			return View();
		}
	}
}
