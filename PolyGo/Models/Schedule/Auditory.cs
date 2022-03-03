using SQLite;

namespace PolyGo.Models.Schedule
{
  [Table("Auditories")]
  public class Auditory
  {
    [PrimaryKey, AutoIncrement, Column("main_id")]
    public int ID { get; set; } // ID for SQLite
    public int IjLessonID { get; set; }
    public int id { get; set; } // ID from json code of page
    public string name { get; set; }

    [Ignore]
    public Building building { get; set; }

    //Building for SQLite
    public int building_id
		{
      get 
      { 
        return building.id;
      }

			set
			{
        building.id = value;
			}
		}
    public string building_name
    {
      get
      {
        return building.name;
      }

      set
      {
        building.name = value;
      }
    }
    public string building_abbr
    {
      get
      {
        return building.abbr;
      }

      set
      {
        building.abbr = value;
      }
    }

    public string building_address
    {
      get
      {
        return building.address;
      }

      set
      {
        building.address = value;
      }
    }

    public Auditory()
		{
      building = new Building();
    }
  }
}
