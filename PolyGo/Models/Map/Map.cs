using System;
using SkiaSharp;
using System.IO;
using System.Reflection;

namespace PolyGo.Models.Map
{
  public class Map
  {
    private string resourceID = "PolyGo.Resources.map.mb_floor_1_image.png";
     
    private SKBitmap bitmap;

    public Map()
    {
      try
      {
        Assembly assembly = this.GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly.GetManifestResourceStream(resourceID))
        {
          bitmap = SKBitmap.Decode(stream);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }
  }
}
