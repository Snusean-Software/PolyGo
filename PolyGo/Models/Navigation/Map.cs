using System;
using System.Collections.Generic;
using SkiaSharp;
using System.Reflection;
using Xamarin.Forms;
using System.IO;

namespace PolyGo.Models.Navigation
{
  public class Map
  {
    /// <summary>
    /// Id of map
    /// </summary>
    public int MapID { get; set; }

    /// <summary>
    /// Count of floors in building
    /// </summary>
    private int FloorCount { get; set; }

    /// <summary>
    /// Identifiers of map images
    /// imageIds[0] - id of first floor map image
    /// id format - assemblyName.folder1.folder2.fileName
    /// </summary>
    private List<string> ImagesIds { get; set; }

    /// <summary>
    /// Bitmap for every image
    /// </summary>
    private List<SKBitmap> Bitmaps { get; set; }

    /// <summary>
    /// All nodes of map
    /// </summary>
    private Dictionary<int, Node> Nodes { get; set; }

    /// <summary>
    /// Graph out of all nodes of map
    /// </summary>
    private Graph MapGraph { get; set; }

    /// <summary>
    /// Create map out of images. imagesIds[0] - image of first floor
    /// </summary>
    /// <param name="imagesIds"></param>
    public Map(int mapID, List<string> imagesIds)
    {
      ImagesIds = new List<string>(imagesIds);
      FloorCount = ImagesIds.Count;
      MapID = mapID;
      Bitmaps = new List<SKBitmap>();
      Assembly assembly = this.GetType().GetTypeInfo().Assembly;
      foreach (var id in ImagesIds)
      {
        Bitmaps.Add(SKBitmap.Decode(assembly.GetManifestResourceStream(id)));
      }

      if (App.MpDatabase.Empty)
      {
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
          }
        }
      }
      MapGraph = new Graph(MapID);
      Nodes = new Dictionary<int, Node>();
      foreach (var node in App.MpDatabase.getNodes(MapID))
      {
        Nodes[node.ID] = node;
      }
    }

    /// <summary>
    /// Draw path on map beetween two nodes
    /// </summary>
    /// <param name="start">Start node id of path</param>
    /// <param name="end">End node id of path</param>
    /// <returns>False if path doesn't exist</returns>
    public bool drawPath(int start, int end)
    {
      var path = MapGraph.findPath(start, end);
      if (path == null)
      {
        return false;
      }

      foreach (var item in path)
      {
        Console.WriteLine(item);
      }
  
      int NodesDrawen = 0;
      while (NodesDrawen + 1 != path.Count)
      {
        using (SKCanvas bitmapCanvas =
          new SKCanvas(Bitmaps[Nodes[path[NodesDrawen]].Floor - 1]))
        {
          var painter = new SKPaint
          {
            IsAntialias = true,
            Color = SKColors.Red,
            Style = SKPaintStyle.Fill
          };
          while (NodesDrawen + 1 != path.Count &&
            Nodes[path[NodesDrawen]].Floor == Nodes[path[NodesDrawen + 1]].Floor)
          {
            Node a = Nodes[path[NodesDrawen]];
            Node b = Nodes[path[NodesDrawen + 1]];
            if (Math.Abs(a.X - b.X) > Math.Abs(a.Y - b.Y))
            {
              bitmapCanvas.DrawRect(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y),
                Math.Abs(a.X - b.X), 5, painter);
            }
            else
            {
              bitmapCanvas.DrawRect(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y),
                5, Math.Abs(a.Y - b.Y), painter);
            }
            Console.WriteLine("IM HERE " + NodesDrawen);
            NodesDrawen++;
          }
          if (NodesDrawen + 1 != path.Count)
          {
            NodesDrawen++;
          }
        }
      }
      
      return true;
    }

    /// <summary>
    /// Clear all floor images from paths
    /// </summary>
    public void clearMap()
    {
      Bitmaps = new List<SKBitmap>();
      Assembly assembly = this.GetType().GetTypeInfo().Assembly;
      foreach (var id in ImagesIds)
      {
        Bitmaps.Add(SKBitmap.Decode(assembly.GetManifestResourceStream(id)));
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="floor">Floor if map has many floors, default floor = 1</param>
    /// <returns>ImageSource of floor's map</returns>
    public ImageSource getMapImageSource(int floor = 1)
    {
      return ImageSource.FromStream(
        () => Bitmaps[floor - 1].Encode(SKEncodedImageFormat.Png, 0).AsStream());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="floor">Floor if map has many floors, default floor = 1</param>
    /// <returns>Stream of image of floor's map</returns>
    public Stream getMapStream(int floor = 1)
    {
      return Bitmaps[floor - 1].Encode(SKEncodedImageFormat.Png, 0).AsStream();
    }
  }
}
