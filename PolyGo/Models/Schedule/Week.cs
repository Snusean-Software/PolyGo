using System;
using System.Collections.Generic;
using SQLite;

namespace PolyGo.Models.Schedule
{

	[Table("Weeks")]
	public class Week
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int InnerJoinID { get; set; }
		public bool IsEven { get; set; }

		[Ignore]
		public List<Day> Days { get; set; }
		public Day Start
		{ 
			get
			{
				Day start = Days[0];
				switch(start.DateNumWeek)
				{
					case 1:
						return start;
					default:
						DateTime dt = new DateTime(Convert.ToInt32(start.Year), start.DateNumMonth, start.DateNum);
						var newDt = dt.AddDays((-1.0) * (start.DateNumWeek - 1));

						start.DateNum = newDt.Day;
						start.DateNumWeek = 1;
						start.DateNumMonth = newDt.Month;
						start.Year = newDt.Year;
						return start;
				}
			} 
		}
		public Day End
		{
			get
			{
				Day end = Days[Days.Count - 1];
				switch (end.DateNumWeek)
				{
					case 7:
						return end;
					default:
						DateTime dt = new DateTime(Convert.ToInt32(end.Year), end.DateNumMonth, end.DateNum);
						var newDt = dt.AddDays(7 - end.DateNumWeek);

						end.DateNum = newDt.Day;
						end.DateNumWeek = 7;
						end.DateNumMonth = newDt.Month;
						end.Year = newDt.Year;
						return end;
				}
			}
		}

		public Week()
		{
			Days = new List<Day>();
		}
	}
}
