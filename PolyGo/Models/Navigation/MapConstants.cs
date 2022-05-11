using System.Collections.Generic;

namespace PolyGo.Models.Navigation
{
  public static class MapConstants
  {
    public enum Floor : int
    {
      First = 0,
      Second = 1,
      Third = 2,
      Fourth = 3,
      Five = 4,
      Sixth = 5,
    }

    public enum MapID : int
    {
      mMainBuilding = 1,
      m11corpus = 2,
    }

    public static Dictionary<MapID, string> CorpusNames = new Dictionary<MapID, string>();

    public static List<string> ImMainBuilding = new List<string>
    {
      "PolyGo.Resources.map.mb.floor_1.png",
      "PolyGo.Resources.map.mb.floor_2.png",
      "PolyGo.Resources.map.mb.floor_3.png"
    };

    static MapConstants()
    {
      CorpusNames[MapID.mMainBuilding] = "Главное здание";
      CorpusNames[MapID.m11corpus] = "11 корпус";
    }

    public static List<string> ImgSource(MapID mapID)
    {
      switch (mapID)
      {
        case MapID.mMainBuilding:
          return ImMainBuilding;
        case MapID.m11corpus:
          return null;
        default:
          return null;
      }
    }
  }
}