using System;
using System.IO;

using PolyGo.SupportFuncs;
using Xamarin.Essentials;

namespace PolyGo
{
  public static class Constants
  {
    public const string ScheduleDatabaseFilename = "ScheduleDatabase.db3";
    public const string GroupsDatabaseFilename = "GroupsDatabase.db3";
    public const string AccountFilename = "Account.txt";

    public static string RefToSchedule = "https://ruz.spbstu.ru/faculty/124/groups/34478";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    //public static string Year = (Connectivity.NetworkAccess == NetworkAccess.Internet) ? ScheduleSupportFuncs.ParseYear() : "2022";
   
    public static string DatabasePath
    {
      get
      {
        var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(basePath, ScheduleDatabaseFilename);
      }
    }

    public static string AccountPath
    {
      get
      {
        var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(basePath, AccountFilename);
      }
    }
  }
}
