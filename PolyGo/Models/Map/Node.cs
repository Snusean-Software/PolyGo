using System.Collections.Generic;

namespace PolyGo.Models.Map
{
  /// <summary>
  /// This class represents node of graph and also contains specific info about node 
  /// </summary>
  public class Node
  {
    /// <summary>
    /// Every node must have unique Id for correct work
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Every adjacent node seems as tuple of
    /// adjacent node id and weight of edge to this node
    /// </summary>
    public List<(int, float)> adjacentNodes { get; set; }

    /// <summary>
    /// Contains all specific info about node
    /// </summary>
    public NodeInfo Info { get; set; }

    /// <summary>
    /// Creates node of graph
    /// </summary>
    /// <param name="id">unique id of node</param>
    /// <param name="adcNodes">list of adjacent nodes. 
    /// Every adjacent node seems as tuple of
    /// adjacent node id and weight of edge to this node.
    /// Wieght of edge must be positive</param>
    /// <param name="info">all specific info about node</param>
    public Node(int id, List<(int, float)> adcNodes, NodeInfo info)
    {
      Id = id;
      adjacentNodes = new List<(int, float)>(adcNodes);
      Info = new NodeInfo(info);
    }
  }

  /// <summary>
  /// This class contains all info about node as classroom number and etc
  /// </summary>
  public class NodeInfo
  {
    public NodeInfo()
    {
    }
    public NodeInfo(NodeInfo other)
    {
    }
  }
}
