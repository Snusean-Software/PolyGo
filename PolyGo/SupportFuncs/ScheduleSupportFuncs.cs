using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using PolyGo.Models.Schedule;

namespace PolyGo.SupportFuncs
{
	internal static class ScheduleSupportFuncs
	{

		/// <summary>
		/// Parse Internet page with JSON code and save it to schedule database
		/// </summary>
		/// <param name="url">Url of Internet page to parse</param>
		/// <returns>Week from parsed page</returns>
		public static async Task<Week> ParseWeek(string url)
		{
			HttpClient httpClient = new HttpClient();
			HttpResponseMessage response =
					(await httpClient.GetAsync(url)).EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			var root = JsonConvert.DeserializeObject<Root>(responseBody);

		  App.Database.SaveRoot(root);

			return root.week;
		}


		/// <summary>
		/// Counts the starting day of the week
		/// </summary>
		/// <param name="day">String with date of start day of week</param>
		/// <returns>Tuple with year-day-month date of start day</returns>
		private static (int year, int day, int month) getWeekDate(string day)
		{
			var nums = day.Split(new char[] { '-', '.' });
			(int year, int day, int month) res;
			res.year = Convert.ToInt32(nums[0]);
			res.month = Convert.ToInt32(nums[1]);
			res.day = Convert.ToInt32(nums[2]);

			return res;
		}

		/// <summary>
		/// Create url to schedule of week with a given starting day 
		/// </summary>
		/// <param name="firstDay">Start day of week</param>
		/// <returns>Url to schedule of week with given start day</returns>
		private static string GetWeekURL(string firstDay)
		{
			var date = getWeekDate(firstDay);
			return Constants.RefToSchedule + "?date=" + date.year.ToString() + '-' 
					+ date.month.ToString() + '-' + date.day.ToString();
		}

		/// <summary>
		/// Change the current schedule week to a new
		/// </summary>
		/// <param name="week">Current week
		/// !!!FUNC CHANGES THIS OBJECT</param>
		/// <param name="numOfWeeks">How many weeks to add to the current one</param>
		/// <returns>Url of new week</returns>
		public static string ChangeWeekUrl(Week week, int numOfWeeks)
		{
			var temp = week;
			var date = getWeekDate(temp.date_start);
			DateTime dt = new DateTime(date.year, date.month, date.day);
			var newStart = dt.AddDays(7.0 * numOfWeeks);
			var newEnd = dt.AddDays(7.0 * numOfWeeks + 6.0); // End day of new week

			temp.date_start = newStart.Year.ToString() + '.' 
					+ newStart.Month.ToString() + '.' + newStart.Day.ToString();

			temp.date_end = newEnd.Year.ToString() + '.'
					+ newEnd.Month.ToString() + '.' + newEnd.Day.ToString();

			temp.is_odd ^= false;

			return GetWeekURL(temp.date_start);
		}
	}
}
