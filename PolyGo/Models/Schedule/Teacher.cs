using SQLite;

namespace PolyGo.Models.Schedule
{
  [Table("Teachers")]
  public class Teacher
  {
    [PrimaryKey, AutoIncrement, Column("main_id")]
    public int ID { get; set; } // ID for SQLite
    public int IjLessonID { get; set; }
    public int id { get; set; } // ID from json code of page
    public int oid { get; set; }
    public string full_name { get; set; }
    public string first_name { get; set; }
    public string middle_name { get; set; }
    public string last_name { get; set; }
    public string grade { get; set; }
    public string chair { get; set; }
  }
}