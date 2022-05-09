using SQLite;

namespace PolyGo.Models.Navigation
{
  /// <summary>
  /// This class represents orientiert edge of graph with weight
  /// </summary>
  [Table("Edges")]
  public class Edge
  {
    [PrimaryKey, AutoIncrement]
    public int DB_ID { get; set; }

    public int StartNodeId { get; set; }

    public int EndNodeId { get; set; }

    public float Weight { get; set; }

    public int MapID { get; set; }

    public Edge() { }

    public Edge(int startId, int endId, float weight)
    {
      StartNodeId = startId; 
      EndNodeId = endId;
      Weight = weight;
    }

    public Edge(Edge other)
    {
      StartNodeId = other.StartNodeId;
      EndNodeId = other.EndNodeId; 
      Weight = other.Weight;
    }
  }
}
