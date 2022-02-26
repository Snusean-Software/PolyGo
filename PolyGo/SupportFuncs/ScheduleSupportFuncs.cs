using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

using PolyGo.Models.Schedule;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace PolyGo.SupportFuncs
{
	internal static class ScheduleSupportFuncs
	{
		private static string getResponse(string url)
		{
			StringBuilder sb = new StringBuilder();
			byte[] buf = new byte[8192];
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream resStream = response.GetResponseStream();
			int count;
			do
			{
				count = resStream.Read(buf, 0, buf.Length);
				if (count != 0)
				{
					var sCurrent = Encoding.Default.GetString(buf, 0, count);
					if (sCurrent.Contains("footer"))
					{
						sb.Append(Encoding.Default.GetString(buf, 0, count));
						break;
					}
					else sb.Append(Encoding.Default.GetString(buf, 0, count));
				}
			}
			while (count > 0);
			return sb.ToString();
		}

		private static int getNumOfMonth(string sMonth)
		{
			switch (sMonth)
			{
				case "янв.,":
					return 1;
				case "февр.,":
					return 2;
				case "мар.,":
					return 3;
				case "апр.,":
					return 4;
				case "мая,":
					return 5;
				case "июня,":
					return 6;
				case "июля,":
					return 7;
				case "авг.,":
					return 8;
				case "сент.,":
					return 9;
				case "окт.,":
					return 10;
				case "нояб.,":
					return 11;
				case "дек.,":
					return 12;

				default:
					return -1;
			}
		}
		private static int getNumInWeek(string sWeekDay)
		{
			switch (sWeekDay)
			{
				case "пн":
					return 1;
				case "вт":
					return 2;
				case "ср":
					return 3;
				case "чт":
					return 4;
				case "пт":
					return 5;
				case "сб":
					return 6;
				case "вс":
					return 7;

				default:
					return -1;
			}
		}

		private static (int num, int numWeek, int numMonth) parseDate(string sDate)
		{
			var dateParts = sDate.Split(' ');
			(int num, int numWeek, int numMonth) date;

			date.num = Convert.ToInt32(dateParts[0]); ;
			date.numMonth = getNumOfMonth(dateParts[1]);
			date.numWeek = getNumInWeek(dateParts[2]);

			return date;
		}
		private static ((string start, string end) interval, string title) parseLessonSubject(string sLessonSubject)
		{
			((string start, string end) interval, string title) lessonSubject = default;

			var subjects = sLessonSubject.Split(new char[] { ' ' }, 2);

			var time = subjects[0].Split('-');
			lessonSubject.interval.start = time[0];
			lessonSubject.interval.end = time[1];

			lessonSubject.title = subjects[1];

			return lessonSubject;
		}

		private static (int DateNum, int DateNumMonth, int Year) getNumOfStartDayOfWeek(Day day)
		{
			switch (day.DateNumWeek)
			{
				case 1:
					return (day.DateNum, day.DateNumMonth, day.Year);
				default:
					DateTime dt = new DateTime(day.Year, day.DateNumMonth, day.DateNum);
					var newDt = dt.AddDays((-1.0) * (day.DateNumWeek - 1));

					return (newDt.Day, newDt.Month, newDt.Year);
			}
		}
		public static Week ParseWeek(string url)
		{
			Week week = new Week();

			string pageCode = getResponse(url);

			var htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(pageCode);

			foreach (var htmlDay in htmlDoc.DocumentNode.SelectNodes(".//li[@class='schedule__day']"))
			{
				Day day = new Day();
				var Date = parseDate(htmlDay.SelectSingleNode(".//div[@class='schedule__date']").InnerText);
				day.DateNum = Date.num;
				day.DateNumWeek = Date.numWeek;
				day.DateNumMonth = Date.numMonth;

				var sYear = htmlDoc.DocumentNode.SelectSingleNode(".//a[@class='printBtn']")
																							 .GetAttributeValue("href", null);
				day.Year = Convert.ToInt32(Regex.Match(sYear, @"date=\d{4,}").ToString().Substring(5));

				var start = getNumOfStartDayOfWeek(day);
				string monthStart = start.DateNumMonth > 9 ? start.DateNumMonth.ToString() : "0" + start.DateNumMonth;
				string innerJoinID = start.DateNum.ToString() + monthStart + day.Year;	
				day.InnerJoinWeekID = Convert.ToInt32(innerJoinID);
				week.InnerJoinID = Convert.ToInt32(innerJoinID);

				foreach (var htmlLesson in htmlDay.SelectNodes(".//ul/li[@class='lesson']"))
				{
					Lesson lesson = new Lesson();
					string monthDay = day.DateNumMonth > 9 ? day.DateNumMonth.ToString() : "0" + day.DateNumMonth;
					var innerJoinDayID = day.DateNum.ToString() + monthDay + day.Year;
					lesson.InnerJoinDayID = Convert.ToInt32(innerJoinDayID);
					day.InnerJoinLessonID = Convert.ToInt32(innerJoinDayID);

					lesson.Title = parseLessonSubject(htmlLesson.SelectSingleNode(".//div[@class='lesson__subject']").InnerText).title;
					var TimeInterval =
						parseLessonSubject(htmlLesson.SelectSingleNode(".//div[@class='lesson__subject']").InnerText).interval;
					lesson.Start = TimeInterval.start;
					lesson.End = TimeInterval.end;	

					lesson.Type = htmlLesson.SelectSingleNode(".//div[@class='lesson__type']").InnerText;

					var groups = htmlLesson.SelectSingleNode(".//div[@class='lesson-groups__list']");
					foreach (var gr in groups.SelectNodes(".//a"))
					{
						lesson.Groups += gr.InnerText + " ";
					}
					lesson.Place = htmlLesson.SelectSingleNode(".//div[@class='lesson__places']").InnerText;

					try // Не всегда назначен преподаватель
					{
						lesson.Teacher = htmlLesson.SelectSingleNode(".//div[@class='lesson__teachers']").InnerText.Trim();
					}
					catch
					{
						lesson.Teacher = "";
					}

					try // Не всегда есть ссылка на СДО
					{
						lesson.SDL = htmlLesson.SelectSingleNode(".//div[@class='lesson__resource_links']")
																		.SelectSingleNode(".//a").GetAttributeValue("href", null);
					}
					catch
					{
						lesson.SDL = "";
					}

					day.Lessons.Add(lesson);
					App.Database.SaveLesson(lesson);
				}

				week.Days.Add(day);
				App.Database.SaveDay(day);
			}


			week.IsEven = true;
			return week;
		}
		public static string GetWeekURL(Day first)
		{
			return Constants.RefToSchedule + "?date=" + first.Year + '-' + first.DateNumMonth + '-' + first.DateNum;
		}
		public static Day ChangeWeek(Day day, int numOfWeeks)
		{
			DateTime dt = new DateTime(day.Year, day.DateNumMonth, day.DateNum);
			var newDt = dt.AddDays(7.0 * numOfWeeks);

			day.DateNum = newDt.Day;
			day.DateNumWeek = 1;
			day.DateNumMonth = newDt.Month;
			day.Year = newDt.Year;

			return day;
		}
	}
}
