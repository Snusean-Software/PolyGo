using System.Collections.Generic;
using SQLite;

namespace PolyGo.Models.Schedule
{
  [Table("Lessons")]
  public class Lesson
  {
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int IjDaysID { get; set; }
    public string subject { get; set; }
    public string subject_short { get; set; }
    public int type { get; set; }
    public string additional_info { get; set; }
    public string time_start { get; set; }
    public string time_end { get; set; }
    public int parity { get; set; }

    [Ignore]
    public TypeObj typeObj { get; set; }

    [Ignore]
    public List<Group> groups { get; set; }

    [Ignore]
    public List<Teacher> teachers { get; set; }

    [Ignore]
    public List<Auditory> auditories { get; set; }
    public string webinar_url { get; set; }
    public string lms_url { get; set; }

    //TypeObj for SQLite (SQLite does not support non-standard types)
    public int typeObj_id
		{
			get
			{
        return typeObj.id;
			}

			set
			{
        typeObj.id = value;
			}
		}
    public string typeObj_name
    {
      get
      {
        return typeObj.name;
      }

      set
      {
        typeObj.name = value;
      }
    }
    public string typeObj_abbr
    {
      get
      {
				return typeObj.name;
      }

      set
      {
        typeObj.name = value;
      }
    }
    public string GroupsString
		{ 
          get
		    {
            string result = string.Empty;
             int c = 1;
        
            foreach (var gr in groups)
				{
          string sep = " ";
          if (c % 3 == 0) sep = "\r\n";
          result += gr.name + sep;
          ++c;
				}
            return result;
		    }
        }
    public string Location
		{
			get
			{
        if (auditories.Count != 0)
          return auditories[0].building_name + ", ауд. " + auditories[0].name;
        else
          return null;
      }
		}
    public string Teacher
        {
            get
            {
                if(teachers.Count != 0) 
                    return teachers[0].full_name;
                else 
                    return null;
            }
        }
    public Lesson()
	{
      groups = new List<Group>();
      teachers = new List<Teacher>(); 
      auditories = new List<Auditory>();

      typeObj = new TypeObj();
	}
  }
}