using System;
using System.Collections.Generic;
using System.Text;

namespace PolyGo.Models.Schedule
{
	internal class Lesson
	{
		public string Title { get; set; }
		public string Type { get; set; }
		public Tuple<string, string> TimeInterval { get; set; }
		public List<string> Groups { get; set; }
		public string Teacher { get; set; }
		public string Place { get; set; }
		public string SDL { get; set; } //СДО (ссылка)
	}
}
