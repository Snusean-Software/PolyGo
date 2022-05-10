using System;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

using PolyGo.Models.Schedule;
using PolyGo.Data;

namespace PolyGo.SupportFuncs
{
	internal static class ScheduleSupportFuncs
	{

		/// <summary>
		/// Parse Internet page with JSON code and save it to schedule database
		/// </summary>
		/// <param name="url">Url of Internet page to parse</param>
		/// <returns>Week from parsed page</returns>
		public static async Task<Root> ParseWeek(string url, bool needToStore = true)
		{
			HttpClient httpClient = new HttpClient();
			HttpResponseMessage response =
					(await httpClient.GetAsync(url)).EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			var root = JsonConvert.DeserializeObject<Root>(responseBody);

			if (needToStore && !App.SchdlDatabase.checkWeekInDB(root.week_date_start))
			{
				App.SchdlDatabase.SaveRoot(root);
			}

			return root;
		}


		/// <summary>
		/// Counts the starting day of the week
		/// </summary>
		/// <param name="day">String with date of start day of week</param>
		/// <returns>Tuple with year-day-month date of start day</returns>
		public static (int year, int day, int month) getWeekDate(string day)
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
		public static string GetWeekURL(string firstDay)
		{
			var date = getWeekDate(firstDay);
			var a = Constants.RefToSchedule + "/?date=" + date.year.ToString() + '-'
					+ date.month.ToString() + '-' + date.day.ToString();
			return Constants.RefToSchedule + "/?date=" + date.year.ToString() + '-'
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
		/// <summary>
		/// Parce page of faculty. Add all groups from this faculty to database
		/// </summary>
		/// <param name="number">Faculty id for URL</param>
		public static void ParceFacultyGroups(string number)
		{
			string url = "https://ruz.spbstu.ru";
			url += "/faculty/" + number + "/groups";
			string pageCode = getResponse(url, "window.__INITIAL_STATE__", "\\script");
			var htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(pageCode);

			foreach (var scriptCode in htmlDoc.DocumentNode.SelectNodes(".//script"))
			{
				if (scriptCode.InnerText.Trim().StartsWith("window.__INITIAL_STATE__"))
				{
					string id_s = "\"id\":";
					string name_s = "\"name\":";
					string group_number_s = "\"" + @"\w??\d+?/\d+?" + "\",";

					string pattern = id_s + @"\d+?," + name_s + group_number_s;
					var groupMatches = Regex.Matches(scriptCode.InnerText, pattern);

					foreach (var groupMatch in groupMatches)
					{
						var id_pattern = id_s + @"\d+?,";
						var name_pattern = name_s + group_number_s;

						string groupURL = url + '/' + Regex.Match(groupMatch.ToString(), id_pattern).ToString().Remove(0, 5).Replace(",", "");
						string groupNum = Regex.Match(groupMatch.ToString(), name_pattern).ToString().Remove(0, 7).Replace(",", "");

						FacultyGroup facultyGroup = new FacultyGroup();
						facultyGroup.Name = groupNum;
						facultyGroup.URL = groupURL;

						App.SchdlDatabase.SaveFacultyGroup(facultyGroup);
					}
					break;
				}
			}
		}
		/// <summary>
		/// Parce page of faculties to get id's. Call ParceFacultyGroups for each.
		/// </summary>
		public static void ParceFacultyNumbers()
		{
			var url = "https://ruz.spbstu.ru";
			string pageCode = getResponse(url, "body", "footer");
			var htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(pageCode);

			foreach (var htmlFaculty in htmlDoc.DocumentNode.SelectNodes(".//li[@class='faculty-list__item']"))
			{
				string name = htmlFaculty.SelectSingleNode(".//a[@class='faculty-list__link']").GetAttributeValue("href", null);
				string number = Regex.Match(name, @"\d+").ToString();
				ParceFacultyGroups(number);
			}
		}
		/// <summary>
		/// Parce page by given url.
		/// </summary>
		/// <param name="url">URL of page to parce</param>
		/// <param name="beginning">Word from which function start parcing</param>
		/// <param name="end">Word on which function end parcing</param>
		/// <returns>Parced page</returns>
		private static string getResponse(string url, string beginning, string end)
		{
			StringBuilder sb = new StringBuilder();
			byte[] buf = new byte[8192];
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.ServerCertificateValidationCallback = delegate { return true; };
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream resStream = response.GetResponseStream();
			int count;
			bool isStarted = false;
			do
			{
				count = resStream.Read(buf, 0, buf.Length);
				if (count != 0)
				{
					var sCurrent = Encoding.Default.GetString(buf, 0, count);
					if (!isStarted)
					{
						if (sCurrent.Contains(beginning)) isStarted = true;
					}
					if (isStarted)
					{
						if (sCurrent.Contains(end))
						{
							sb.Append(Encoding.Default.GetString(buf, 0, count));
							break;
						}
						else sb.Append(Encoding.Default.GetString(buf, 0, count));
					}
				}
			}
			while (count > 0);
			return sb.ToString();
		}
	}
}
