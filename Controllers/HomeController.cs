﻿using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_assignments.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Contact()
		{


			return View();
		}
		
		public IActionResult Projects()
		{


			return View();
		}
	}
}
