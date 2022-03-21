using System;
using System.Collections.Generic;
using SkiaSharp;
using System.IO;
using System.Reflection;

namespace PolyGo.Models.Map
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
    /// 
    /// </summary>
    private List<SKBitmap> Bitmaps { get; set; }

    public Map(List<string> imagesIds)
    {
      ImagesIds = new List<string>(imagesIds);
      Bitmaps = new List<SKBitmap>();
      Assembly assembly = this.GetType().GetTypeInfo().Assembly;
      foreach (var id in imagesIds)
      {
        Bitmaps.Add(SKBitmap.Decode(assembly.GetManifestResourceStream(id)));
      }
    }

    public bool drawPath(/*Node start, Node end*/)
    {
      Assembly assembly = this.GetType().GetTypeInfo().Assembly;
      using (Stream stream = assembly.GetManifestResourceStream(ImagesIds[0]))
      {
        Bitmaps.Add(SKBitmap.Decode(stream));
        using (SKCanvas bitmapCanvas = new SKCanvas(Bitmaps[0]))
        {
          bitmapCanvas.Clear(SKColors.White);

          var painter = new SKPaint
          {
            IsAntialias = true,
            Color = new SKColor(255, 0, 0),
            Style = SKPaintStyle.Fill
          };

          bitmapCanvas.DrawCircle(500, 1000, 200, painter);
        }
      }
      return true;
    }

    public Stream getMapStream(int floor)
    {
      return Bitmaps[floor].Encode(SKEncodedImageFormat.Png, 0).AsStream();
    }
  }
}
