using System;
using System.Collections.Generic;
using System.Text;

namespace PolyGo.Models.Schedule
{
	internal class Day
	{
		public int Num { get; set; }
		public int NumWeek { get; set; }
		public int NumMonth { get; set; }
		public List<Lesson> Lessons { get; set; }
	}
}
