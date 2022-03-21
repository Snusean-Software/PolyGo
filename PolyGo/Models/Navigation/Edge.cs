
namespace PolyGo.Models.Navigation
{
  /// <summary>
  /// This class represents orientiert edge of graph with weight
  /// </summary>
  public class Edge
  {
    public int StartNodeId { get; set; }

    public int EndNodeId { get; set; }

    public float Weight { get; set; }

    public Edge(int startId, int endId, float weight)
    {
      StartNodeId = startId; 
      EndNodeId = endId;
      Weight = weight;
    }
  }
}
