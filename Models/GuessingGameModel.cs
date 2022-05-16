using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace ASP_NET_assignments.Models
{
	public class GuessingGameModel
	{
		public const string GAMENUMBER_KEY = "GameNumber";
		public const string GUESSES_KEY = "Guesses";
		public const string VICTORY_KEY = "Victory";
		public const string HIGHSCORE_KEY = "Highscores";

		private static readonly Random rand = new Random();


		public static void StartNewSession(HttpContext context)
		{
			context.Session.Clear();

			context.Session.SetInt32(GAMENUMBER_KEY, rand.Next(1, 101));
			context.Session.SetString(GUESSES_KEY, "");
			context.Session.SetInt32(VICTORY_KEY, 0);
		}

		public static bool IsNewGame(HttpContext context)
		{
			return context.Session.TryGetValue("GameID", out _);
		}
		public static bool CheckGuess(HttpContext context, int guess)
		{
			if (guess < 1 || guess > 100)
			{
				return false;
			}
			string guesses;
			
			
			int gameNumber = context.Session.GetInt32(GAMENUMBER_KEY).Value;

			if(guess == gameNumber)
			{
				GameFinish(context);
				guesses = $"{context.Session.GetString(GUESSES_KEY)}{guess} Correct!";
			}
			else if(guess < gameNumber)
			{
				guesses = $"{context.Session.GetString(GUESSES_KEY)}{guess} Too low,";
			}
			else
			{
				guesses = $"{context.Session.GetString(GUESSES_KEY)}{guess} Too high,";
			}
			context.Session.SetString(GUESSES_KEY, guesses);
			return true;
		}

		private static void GameFinish(HttpContext context)
		{
			context.Session.SetInt32(VICTORY_KEY, 1);

			if(!context.Request.Cookies.TryGetValue(HIGHSCORE_KEY, out string highscoreCookie))
			{
				CookieBuilder cb = new CookieBuilder();
				cb.Expiration = new TimeSpan(2, 0, 0, 0);
				cb.IsEssential = true;
				cb.Name = HIGHSCORE_KEY;
				cb.SameSite = SameSiteMode.Strict;

				// Finns inget sätt att lägga in custom data i en cookie??
				// HttpCookie cookie["varname"] = "data"; finns i andra versioner av .NET
				// men kan inte hitta någonting motsvarande i .net core
			}


		}
	}
}
