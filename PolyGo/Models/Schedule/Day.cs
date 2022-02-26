using System.Collections.Generic;
using SQLite;

namespace PolyGo.Models.Schedule
{
	[Table("Days")]
	public class Day
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int InnerJoinWeekID { get; set; } //StartWeekDateNum + StartWeekDateNumMonth + Year (ex: 20072022 - 20-26 jule 2022)
		public int InnerJoinLessonID { get; set; } //DateNum + MonthNum + Year (ex: 21072022 - 21 jule 2022)
		public int DateNum { get; set; }
		public int DateNumWeek { get; set; }
		public int DateNumMonth { get; set; }
		public int Year { get; set; }

		[Ignore]
		public List<Lesson> Lessons { get; set; }

		public Day()
		{
			Lessons = new List<Lesson>();
		}
	}
}
