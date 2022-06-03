using System;
using System.IO;

namespace PolyGo
{
  public static class Constants
  {
    public const string ScheduleDatabaseFilename = "ScheduleDatabase.db3";
    public const string MapDatabaseFilename = "MapDatabase.db3";
    public const string AccountFilename = "Account.txt";

    public const string FacultyGroupsDatabaseFilename = "FacultyGroupsDatabase.db3";
    public const string RoutsDatabaseFilename = "RoutsDatabase.db3";

    private static string basePath =
      Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public static string RefToSchedule
		{
			get
			{
        var accInfo = File.ReadAllText(AccountPath).Trim();
        if (!accInfo.Contains("Link:")) return "BASED GIGACHAD";

        string[] words = accInfo.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < words.Length; ++i)
        {
          if (words[i].StartsWith("Link:"))
          {
            return "https://ruz.spbstu.ru/api/v1/ruz/scheduler/" + words[i].Substring(6);
          }
        }

        return "base";
			}
		}

    public static string IDcurrentGroup
		{
			get
			{
        var accInfo = File.ReadAllText(AccountPath).Trim();
        if (!accInfo.Contains("Link:")) return "BASED GIGACHAD";

        string[] words = accInfo.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < words.Length; ++i)
        {
          if (words[i].StartsWith("Link:"))
          {
            return words[i].Substring(6);
          }
        }

        return "base";
      }
		}
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

    //For adapter classes
    public static string FacultyGroupsDatabasePath
    {
      get
      {
        return Path.Combine(basePath, FacultyGroupsDatabaseFilename);
      }
    }

    public static string RoutsDatabasePath
    {
      get
      {
        return Path.Combine(basePath, RoutsDatabaseFilename);
      }
    }
  }
}
