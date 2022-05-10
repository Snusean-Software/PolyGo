using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;

using SkiaSharp;

namespace PolyGo.Models.Navigation
{
  public class Map
  {
    /// <summary>
    /// Id of map
    /// </summary>
    public MapConstants.MapID MapID { get; set; }

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
    public Map(MapConstants.MapID mapID)
    {
      MapID = mapID;
      Bitmaps = new List<SKBitmap>();
      Assembly assembly = this.GetType().GetTypeInfo().Assembly;
      foreach (var id in MapConstants.ImgSource(MapID))
      {
        Bitmaps.Add(SKBitmap.Decode(assembly.GetManifestResourceStream(id)));
      }
      if (App.MpDatabase.Empty)
      {
        App.MpDatabase.initDatabase();
      }
      MapGraph = new Graph(MapID);
      Nodes = new Dictionary<int, Node>();
      foreach (var node in App.MpDatabase.getNodes((int)MapID))
      {
        Nodes[node.ID] = node;
      }
    }

    /// <summary>
    /// Draw path on map beetween two nodes
    /// </summary>
    /// <param name="start">Start classroom of path</param>
    /// <param name="end">End calssroom of path</param>
    /// <returns>False if path doesn't exist</returns>
    public bool drawPath(string start, string end)
    {
      var a = App.MpDatabase.getNode(start, (int)MapID);
      var b = App.MpDatabase.getNode(end, (int)MapID);
      if (a != null && b != null)
      {
        return drawPath(a.ID, b.ID);
      }
      Console.WriteLine("ERROR! No nodes with that classroom!");
      return false;
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
      foreach (var id in MapConstants.ImgSource(MapID))
      {
        Bitmaps.Add(SKBitmap.Decode(assembly.GetManifestResourceStream(id)));
      }
    }

    /// <param name="floor">Floor if map has many floors, default floor = 1</param>
    /// <returns>ImageSource of floor's map</returns>
    public ImageSource getMapImageSource(MapConstants.Floor floor)
    {
      return ImageSource.FromStream(
        () => Bitmaps[(int)floor].Encode(SKEncodedImageFormat.Png, 0).AsStream());
    }

    /// <param name="floor">Floor if map has many floors, default floor = 1</param>
    /// <returns>Stream of image of floor's map</returns>
    public Stream getMapStream(MapConstants.Floor floor)
    {
      return Bitmaps[(int)floor].Encode(SKEncodedImageFormat.Png, 0).AsStream();
    }
  }
}
