using System.Collections.Generic;
using SQLite;

namespace PolyGo.Models.Schedule
{
  [Table("Roots")]
  public class Root
  {
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [Ignore]
    public Week week { get; set; }

    [Ignore]
    public List<Day> days { get; set; }

    [Ignore]
    public Group group { get; set; }

    //Week for SQLite (SQLite does not support non-standard types)
    public string week_date_start 
    { 
      get
			{
        return week.date_start;
			}
      
      set
			{
        week.date_start = value; 
			}
    }
    public string week_date_end
    {
      get
      {
        return week.date_end;
      }

      set
      {
        week.date_end = value;
      }
    }
    public bool week_is_odd
    {
      get
      {
        return week.is_odd;
      }

      set
      {
        week.is_odd = value;
      }
    }

    //Group for SQLite (SQLite does not support non-standard types)
    public int group_IjLessonID
		{
			get
			{
        return group.IjLessonID;
			}

			set
			{
        group.IjLessonID = value;
			}
		}
    public int group_id
    {
      get
      {
        return group.id;
      }

      set
      {
        group.id = value;
      }
    }
    public string group_name
    {
      get
      {
        return group.name;
      }

      set
      {
        group.name = value;
      }
    }
    public int group_level
    {
      get
      {
        return group.level;
      }

      set
      {
        group.level = value;
      }
    }
    public string group_type
    {
      get
      {
        return group.type;
      }

      set
      {
        group.type = value;
      }
    }
    public int group_kind
    {
      get
      {
        return group.kind;
      }

      set
      {
        group.kind = value;
      }
    }
    public string group_spec
    {
      get
      {
        return group.spec;
      }

      set
      {
        group.spec = value;
      }
    }
    public int group_year
    {
      get
      {
        return group.year;
      }

      set
      {
        group.year = value;
      }
    }
    public int group_faculty_id
    {
      get
      {
        return group.faculty_id;
      }

      set
      {
        group.faculty_id = value;
      }
    }
    public string group_faculty_name
    {
      get
      {
        return group.faculty_name;
      }

      set
      {
        group.faculty_name = value;
      }
    }
    public string group_faculty_abbr
    {
      get
      {
        return group.faculty_abbr;
      }

      set
      {
        group.faculty_abbr = value;
      }
    }
    public Root()
		{
      days = new List<Day>();
      week = new Week();
      group = new Group();
		}
  }
}