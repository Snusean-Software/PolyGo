using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;

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
    /// Initialize database from text files
    /// </summary>
    /// <returns></returns>
    public bool initDatabase()
    {
      Assembly assembly = this.GetType().GetTypeInfo().Assembly;
      StreamReader reader = new StreamReader(
         assembly.GetManifestResourceStream("PolyGo.Resources.map.graph_nodes.txt"));
      while (!reader.EndOfStream)
      {
        string line = reader.ReadLine();
        var parts = line.Split(' ');
        if (parts.Length == 6)
        {
          var node = new Node();
          node.ID = int.Parse(parts[0]);
          node.MapID = int.Parse(parts[1]);
          node.Classroom = parts[2];
          node.X = int.Parse(parts[3]);
          node.Y = int.Parse(parts[4]);
          node.Floor = int.Parse(parts[5]);
          App.MpDatabase.saveNode(node);
        }
        else
        {
          Console.WriteLine("Error in graph_nodes.txt file syntax!!!");
          return false;
        }
      }
      reader = new StreamReader(
        assembly.GetManifestResourceStream("PolyGo.Resources.map.graph_edges.txt"));
      while (!reader.EndOfStream)
      {
        string line = reader.ReadLine();
        var parts = line.Split(' ');
        if (parts.Length == 4)
        {
          var edge = new Edge();
          edge.StartNodeId = int.Parse(parts[0]);
          edge.EndNodeId = int.Parse(parts[1]);
          edge.Weight = int.Parse(parts[2]);
          edge.MapID = int.Parse(parts[3]);
          App.MpDatabase.saveEdge(edge);
        }
        else
        {
          Console.WriteLine("Error in graph_edges.txt file syntax!!!");
          return false;
        }
      }
      return true;
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

    /// <summary>
    /// Find node by classroom name.
    /// </summary>
    /// <param name="classroom">Classroom to be searched</param>
    /// <param name="mapID">Node will be searched in this map</param>
    /// <returns>All info about node</returns>
    public Node getNode(string classroom, int mapID)
    {
      foreach (var node in database.Table<Node>())
      {
        if (node.Classroom == classroom && node.MapID == mapID)
        {
          return node;
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
