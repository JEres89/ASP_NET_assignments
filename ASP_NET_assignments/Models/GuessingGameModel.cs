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
				guesses = $"{context.Session.GetString(GUESSES_KEY)}{guess} Correct!";
				GameFinish(context, guesses);
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

		private static void GameFinish(HttpContext context, string guesses)
		{
			context.Session.SetInt32(VICTORY_KEY, 1);
			string[][] scores;
			int currentGameGuesses = guesses.Split('\u002C').Length;
			int position = 0;

			if(context.Request.Cookies.TryGetValue(HIGHSCORE_KEY, out string highscoreCookie))
			{
				context.Response.Cookies.Delete(HIGHSCORE_KEY);
				string[] s = highscoreCookie.Split('\u002C');
				scores = new string[s.Length+1][];
				for(int i = 0, pos = 1; i < s.Length; i++, pos++)
				{
					string[] scoreEntry = s[i].Split('\u0020');

					if(position == 0)
					{
						if(currentGameGuesses < Convert.ToInt32(scoreEntry[2]))
						{
							position = pos;
							pos++;
							scores[i] = new string[] { $"{position}:", " Name ", $"{currentGameGuesses}," };
						}
					}
					else
					{
						scoreEntry[0] = $"{pos}:";
					}
					scores[i + Convert.ToInt32(position != 0)] = scoreEntry;
				}
			}
			else
			{
				scores = new string[][] { new string[] { "1:", " Name ", $"{currentGameGuesses}," } };
				
			}

			CookieBuilder cb = new CookieBuilder();
			cb.Expiration = new TimeSpan(2, 0, 0, 0);
			cb.IsEssential = true;
			cb.Name = HIGHSCORE_KEY;
			cb.SameSite = SameSiteMode.Strict;

			context.Response.Cookies.Append(HIGHSCORE_KEY, scores.ToString(), cb.Build(context));
		}
	}
}
