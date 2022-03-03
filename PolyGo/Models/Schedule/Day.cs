using System.Collections.Generic;
using SQLite;

namespace PolyGo.Models.Schedule
{
  [Table("Days")]
  public class Day
	{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int IjRootID { get; set; } 
    public int weekday { get; set; }
    public string date { get; set; }

    [Ignore]
    public List<Lesson> lessons { get; set; }

    public Day()
    {
      lessons = new List<Lesson>();
    }
  }
}