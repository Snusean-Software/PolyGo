using System;
using System.Collections.Generic;
using System.Text;

namespace PolyGo.Models.Schedule
{
	internal class Week
	{
		public bool isEven { get; set; }
		public List<Day> Days { get; set; }
		public Tuple<Day, Day> StartEnd 
		{ 
			get
			{
				return Tuple.Create(Days[0], Days[Days.Count - 1]);
			} 
		}
	}
}
