using System.Collections.Generic;

using SQLite;

using PolyGo.Models.Navigation;

namespace PolyGo.Data
{
  public class MapDatabase
  {
    readonly SQLiteConnection database;
    public MapDatabase()
    {
      database = new SQLiteConnection(Constants.MapDatabasePath);

      database.CreateTable<Node>();
      database.CreateTable<Edge>();
    }

    /// <summary>
    /// If table Nodes is empty
    /// </summary>
    public bool Empty
    {
      get
      {
        return database.Table<Node>().Count() == 0;
      }
    }

    /// <summary>
    /// Saves or updtes node in database
    /// </summary>
    /// <param name="node">Node to be saved</param>
    /// <returns></returns>
    public int saveNode(Node node)
    {
      if (node.DB_ID != 0)
      {
        return database.Update(node);
      }
      else
      {
        return database.Insert(node);
      }
    }

    /// <summary>
    /// Saves or updates edge in database
    /// </summary>
    /// <param name="edge">Edge to be saved</param>
    /// <returns></returns>
    public int saveEdge(Edge edge)
    {
      if (edge.DB_ID != 0)
      {
        return database.Update(edge);
      }
      else
      {
        return database.Insert(edge);
      }
    }

    /// <param name="mapID">Edge will be searched in this map</param>
    /// <param name="startNodeId">Start node of the edge</param>
    /// <returns>List of all edges, that starts with StartNodeId, on map</returns>
    public List<Edge> getEdges(int mapID, int startNodeId)
    {
      var result = new List<Edge>();
      foreach (var edge in database.Table<Edge>())
      {
        if (edge.MapID == mapID && edge.StartNodeId == startNodeId)
        {
          result.Add(edge);
        }
      }
      return result;
    }

    /// <param name="nodeID">Node to be searched</param>
    /// <param name="mapID">Node will be searched in this map</param>
    /// <returns>All info about node</returns>
    public Node getNodeInfo(int nodeID, int mapID)
    {
      foreach (var nodeInfo in database.Table<Node>())
      {
        if (nodeInfo.ID == nodeID && nodeInfo.MapID == mapID)
        {
          var result = new Node(nodeInfo);
          return result;
        }
      }
      return null;
    }

    /// <param name="mapID">Nodes of what map must be returned</param>
    /// <returns>All nodes of specific map</returns>
    public List<Node> getNodes(int mapID)
    {
      List<Node> result = new List<Node>();
      foreach (var node in database.Table<Node>())
      {
        if (node.MapID == mapID)
        {
          result.Add(node);
        }
      }
      return result;
    }
  }
}
