using System.Collections.Generic;

namespace PolyGo.Models.Schedule
{
	internal class Lesson
	{
		public string Title { get; set; }
		public string Type { get; set; }
		(string start, string end) TimeInterval { get; set; }
		public List<string> Groups { get; set; }
		public string Teacher { get; set; }
		public string Place { get; set; }
		public string SDL { get; set; } //СДО (ссылка)
	}
}
