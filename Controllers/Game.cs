using static ASP_NET_assignments.Models.GuessingGameModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_assignments.Controllers
{
	public class Game : Controller
	{
		[HttpGet]
		public IActionResult GuessingGame()
		{

			StartNewSession(HttpContext);

			ViewBag.message = "Welcome to the Guessing Game! \n Make your first guess from 1 to 100";
			
			return View();
		}

		[HttpPost]
		public IActionResult GuessingGame(int guess)
		{
			if(!CheckGuess(HttpContext, guess))
			{
				ViewBag.message = "Invalid guess, must be a number from 1 to 100";
				return View();
			}

			string guesses = HttpContext.Session.GetString(GUESSES_KEY);
			ViewBag.guesses = guesses;

			if(HttpContext.Session.GetInt32(VICTORY_KEY).Value == 1)
			{
				int n = guesses.Split(',').Length;
				ViewBag.message = $"Victory! Congratulations, you win with {n} guesses!";
				ViewBag.win = true;
				return View();
			}

			ViewBag.message = "Make your next guess of a number between and including 1 and 100!";
			return View();
		}
	}
}
