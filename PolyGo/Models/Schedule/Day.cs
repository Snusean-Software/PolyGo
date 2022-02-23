using System.Collections.Generic;

namespace PolyGo.Models.Schedule
{
	internal class Day
	{
		public (int num, int numWeek, int numMonth) Date { get; set; }
		public List<Lesson> Lessons { get; set; }

		public Day()
		{
			Lessons = new List<Lesson>();
		}
	}
}
