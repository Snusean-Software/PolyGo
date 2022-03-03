using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

using PolyGo.Models.Schedule;

namespace PolyGo.SupportFuncs
{
	internal static class ScheduleSupportFuncs
	{
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

		private static (int year, int day, int month) getWeekDate(string day)
		{
			var nums = day.Split(new char[] { '-', '.' });
			(int year, int day, int month) res;
			res.year = Convert.ToInt32(nums[0]);
			res.month = Convert.ToInt32(nums[1]);
			res.day = Convert.ToInt32(nums[2]);

			return res;
		}
		private static string GetWeekURL(string firstDay)
		{
			var date = getWeekDate(firstDay);
			return Constants.RefToSchedule + "?date=" + date.year.ToString() + '-' 
					+ date.month.ToString() + '-' + date.day.ToString();
		}
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
