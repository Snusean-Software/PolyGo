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
    public Map(List<string> imagesIds)
    {
      ImagesIds = new List<string>(imagesIds);
      FloorCount = ImagesIds.Count;
      Bitmaps = new List<SKBitmap>();
      Assembly assembly = this.GetType().GetTypeInfo().Assembly;
      foreach (var id in ImagesIds)
      {
        Bitmaps.Add(SKBitmap.Decode(assembly.GetManifestResourceStream(id)));
      }  
    }

    /// <summary>
    /// Draw path on map beetween two nodes
    /// </summary>
    /// <param name="start">Start of path</param>
    /// <param name="end">End of path</param>
    /// <returns>False if path doesn't exist</returns>
    public bool drawPath(Node start, Node end)
    {
      var path = MapGraph.findPath(start.ID, end.ID);
      if (path == null)
      {
        return false;
      }
  
      int NodesDrawen = 0;
      while (NodesDrawen + 1 != path.Count)
      {
        using (SKCanvas bitmapCanvas =
          new SKCanvas(Bitmaps[Nodes[path[NodesDrawen]].Floor]))
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
              bitmapCanvas.DrawRect((a.X + b.X) / 2, a.Y,
                Math.Abs(a.X - b.X), 10, painter);
            }
            else
            {
              bitmapCanvas.DrawRect(a.X, (a.Y + b.Y) / 2,
                10, Math.Abs(a.Y - b.Y), painter);
            }
            NodesDrawen++;
          }
          if (Nodes[path[NodesDrawen]].Floor == Nodes[path[NodesDrawen + 1]].Floor)
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
