using System.Collections.Generic;
using SQLite;

namespace PolyGo.Models.Navigation
{
  /// <summary>
  /// This class represents a node in graph
  /// </summary>
  [Table("Nodes")]
  public class Node
  {
    /// <summary>
    /// Id for database, this id sets automaticly
    /// </summary>
    [PrimaryKey, AutoIncrement]
    public int DB_ID { get; set; }

    /// <summary>
    /// ID of node, must be set with hands
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// To what map belongs this node
    /// </summary>
    public int MapID { get; set; }

    /// <summary>
    /// Contains number of classroom
    /// </summary>
    public string Classroom { get; set; }

    /// <summary>
    /// X coordinate on map
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Y coordinate on map
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Node's floor on map
    /// </summary>
    public int Floor { get; set; }

    public Node()
    {
    }

    public Node(Node other)
    {
      Classroom = string.Copy(other.Classroom);
      X = other.X;
      Y = other.Y;
      Floor = other.Floor;
    }
  }
}
