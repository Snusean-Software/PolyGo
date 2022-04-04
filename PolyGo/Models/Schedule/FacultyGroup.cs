using SQLite;

namespace PolyGo.Models.Schedule
{
  [Table("FacultyGroups")]
  public class FacultyGroup
  {
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; }
    public string URL { get; set; }
  }
}
