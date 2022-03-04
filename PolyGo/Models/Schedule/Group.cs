using SQLite;

namespace PolyGo.Models.Schedule
{
  [Table("Groups")]
  public class Group
  {
    [PrimaryKey, AutoIncrement, Column("main_id")]
    public int ID { get; set; } // ID for SQLite
    public int IjLessonID { get; set; }
    public int id { get; set; } // ID from json code of page
    public string name { get; set; }
    public int level { get; set; }
    public string type { get; set; }
    public int kind { get; set; }
    public string spec { get; set; }
    public int year { get; set; }

    [Ignore]
    public Faculty faculty { get; set; }

    //Faculty for SQLite (SQLite does not support non-standard types)
    public int faculty_id
		{
			get
			{
        return faculty.id;
			}

			set
			{
        faculty.id = value;
      }
		}
    public string faculty_name
    {
      get
      {
        return faculty.name;
      }

      set
      {
        faculty.name = value;
      }
    }
    public string faculty_abbr
    {
      get
      {
        return faculty.abbr;
      }

      set
      {
        faculty.abbr = value;
      }
    }
    public Group()
    {
      faculty = new Faculty();
    }
  }
}