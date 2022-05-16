using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;

namespace ASP_NET_assignments.Models
{
	public class DoctorModel
	{
		public static string TempCheck(char scale, float temp)
		{
			if(temp == 0)
			{
				return "Du måste skriva ett decimaltal!";
			}
			else
			{
				if(HasFever(scale, temp))
				{
					return "Du har feber!";

				}
				else if(HasHypothermia(scale, temp))
				{
					return "Du är nedkyld!";
				}
				else
				{
					return "Du mår bra.";
				}
			}
		}
		public static bool HasFever(char scale, float temp)
		{
			switch(scale)
			{
				case 'C':
					return temp > 37.5;
				case 'F':
					return temp > 100;
				case 'K':
					return temp > 273.15 + 37.5;
				default:
					return false;
			}
			
		}
		public static bool HasHypothermia(char scale, float temp)
		{
			switch(scale)
			{
				case 'C':
					return temp < 35;
				case 'F':
					return temp < 95;
				case 'K':
					return temp < 273.15 + 35;
				default:
					return false;
			}
			
		}
	}
}
