using System;
using System.IO;

namespace PolyGo
{
  public static class Constants
  {
    public const string ScheduleDatabaseFilename = "ScheduleDatabase.db3";
    public const string MapDatabaseFilename = "MapDatabase.db3";
    public const string GroupsDatabaseFilename = "GroupsDatabase.db3";
    public const string AccountFilename = "Account.txt";

    private static string basePath =
      Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public static string RefToSchedule = "https://ruz.spbstu.ru/api/v1/ruz/scheduler/33742";
    public static string ScheduleDatabasePath
    {
      get
      {
        return Path.Combine(basePath, ScheduleDatabaseFilename);
      }
    }
    public static string MapDatabasePath
    {
      get
      {
        return Path.Combine(basePath, MapDatabaseFilename);
      }
    }
    public static string AccountPath
    {
      get
      {
        return Path.Combine(basePath, AccountFilename);
      }
    }
  }
}
