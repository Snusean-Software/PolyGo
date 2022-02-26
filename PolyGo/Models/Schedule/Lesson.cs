using System.Collections.Generic;
using SQLite;

namespace PolyGo.Models.Schedule
{
	[Table("Lessons")]
	public class Lesson
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int InnerJoinDayID { get; set; } 
		public string Title { get; set; }
		public string Type { get; set; }	
		public string Start { get; set; }
		public string End { get; set; }

		[Ignore]
		public string Groups { get; set; }
		public string Teacher { get; set; }
		public string Place { get; set; }
		public string SDL { get; set; } //СДО (ссылка)
	}
}
