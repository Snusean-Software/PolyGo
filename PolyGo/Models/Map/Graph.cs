using System.Collections.Generic;

namespace PolyGo.Models.Map
{
  public class Graph
  {
    private List<Edge> Edges;
    private List<int> nodeIds;
    private int nodeCount;

    /// <summary>
    /// Create graph out of nodes
    /// </summary>
    /// <param name="nodes">Nodes of graph</param>
    public Graph(List<Node> nodes)
    {
      Edges = new List<Edge>();
      nodeIds = new List<int>();
      nodeCount = nodes.Count;
      foreach (var node in nodes)
      {
        nodeIds.Add(node.Id);
        foreach (var adjacentNode in node.AdjacentNodes)
        {
          Edges.Add(new Edge(node.Id, adjacentNode.Item1, adjacentNode.Item2));
        }
      }
    }

    /// <summary>
    /// Find the shortest path from node with startId to node with endId.
    /// </summary>
    /// <param name="startId">Id of start node in path</param>
    /// <param name="endId">Id of end node in path</param>
    /// <returns>Path as list of nodes. If path doesn't exist returns empty list</returns>
    public List<int> findPath(int startId, int endId)
    {
      // pathWeight[v] - weight of shortest path from start to any vertex v
      Dictionary<int, float> pathWeight = new Dictionary<int, float>();
      // parent[v] - predecessor of vertex v in shortest path
      Dictionary<int, int> parent = new Dictionary<int, int>();
      // is paths changed
      bool changed = true;

      // Initialize containers
      foreach (var nodeId in nodeIds)
      {
        pathWeight[nodeId] = System.Single.MaxValue;
        parent[nodeId] = -1;
      }
      pathWeight[startId] = 0.0f;

      // Find shortest paths
      for (int i = 0; i < nodeCount && changed; ++i)
      {
        changed = false;
        foreach (var edge in Edges)
        {
          if (pathWeight[edge.StartNodeId] != System.Single.MaxValue &&
              pathWeight[edge.EndNodeId] > pathWeight[edge.StartNodeId] + edge.Weight)
          {
            pathWeight[edge.EndNodeId] = pathWeight[edge.StartNodeId] + edge.Weight;
            parent[edge.EndNodeId] = edge.StartNodeId;
            changed = true;
          }
        }
      }

      // Is path exist ?
      if (parent[endId] == -1)
      {
        return new List<int>();
      }

      // Save every node of path
      List<int> path = new List<int>();
      path.Add(endId);
      int curNodeId = endId;
      while (parent[curNodeId] != -1)
      {
        curNodeId = parent[curNodeId];
        path.Add(curNodeId);
      }
      path.Reverse();

      return path;
    }
  }
}
