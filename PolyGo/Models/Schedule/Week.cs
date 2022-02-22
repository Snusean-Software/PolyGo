using System.Collections.Generic;
using SQLite;

namespace PolyGo.Models.Schedule
{
	internal class Week
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public bool isEven { get; set; }
		public List<Day> Days { get; set; }
		public (Day start, Day end) DayInterval
		{ 
			get
			{
				return (Days[0], Days[Days.Count - 1]);
			} 
		}
	}
}
