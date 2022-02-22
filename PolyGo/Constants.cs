using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace PolyGo
{
  public static class Constants
  {
    public const string DatabaseFilename = "ScheduleDatabase.db3";
    public const string AccountFilename = "Account.txt";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath
    {
      get
      {
        var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(basePath, DatabaseFilename);
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
