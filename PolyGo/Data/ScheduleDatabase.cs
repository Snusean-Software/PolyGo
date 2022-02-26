using System.Threading.Tasks;
using System.Collections.Generic;
using SQLite;

using PolyGo.Models.Schedule;
using System;
using System.IO;

namespace PolyGo.Data
{
  public class ScheduleDatabase
	{
    readonly SQLiteConnection database;

    public ScheduleDatabase()
    {
      database = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
      try
      {
        database.CreateTable<Lesson>();
        database.CreateTable<Day>();
        database.CreateTable<Week>();
      }
      catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
    }

    public bool Empty
		{
			get
			{
        return database.Table<Week>().Count() == 0;
      }
		}

    public List<Week> GetWeeks()
    {
      //Get all schedule.
      return database.Table<Week>().ToList();
    }

    public List<Day> GetDays()
    {
      //Get all schedule.
      return database.Table<Day>().ToList();
    }

    public List<Lesson> GetLessons()
    {
      //Get all schedule.
      return database.Table<Lesson>().ToList();
    }

    public Week GetWeek(int id)
    {
      // Get a specific week.
      return database.Table<Week>()
                      .Where(i => i.ID == id)
                      .FirstOrDefault();
    }

    public List<Day> GetAllDaysByID(int IJid)
    {
      List<Day> days = new List<Day>();
      foreach(var day in database.Table<Day>())
			{
        if(day.InnerJoinWeekID == IJid) days.Add(day);
			}
      return days;
    }

    public List<Lesson> GetAllLessonsByID(int IJid)
    {
      List<Lesson> lessons = new List<Lesson>();
      foreach (var day in database.Table<Lesson>())
      {
        if (day.InnerJoinDayID == IJid) lessons.Add(day);
      }
      return lessons;
    }

    public List<Week> GetAllData()
    {
      List<Week> weeks = new List<Week>();
      foreach (var week in database.Table<Week>())
      {
        foreach (Day day in GetAllDaysByID(week.InnerJoinID))
				{
          foreach (Lesson lesson in GetAllLessonsByID(day.InnerJoinLessonID))
					{
            day.Lessons.Add(lesson);
					}
          week.Days.Add(day);
				}
        weeks.Add(week);
      }
      return weeks;
    }

    public int SaveWeek(Week week)
    {
      if (week.ID != 0)
      {
        // Update an existing week.
        return database.Update(week);
      }
      else
      {
        // Save a new week.
        return database.Insert(week);
      }
    }

    public int SaveDay(Day day)
    {
      if (day.ID != 0)
      {
        // Update an existing week.
        return database.Update(day);
      }
      else
      {
        // Save a new week.
        return database.Insert(day);
      }
    }

    public int SaveLesson(Lesson lesson)
    {
      if (lesson.ID != 0)
      {
        // Update an existing week.
        return database.Update(lesson);
      }
      else
      {
        // Save a new week.
        return database.Insert(lesson);
      }
    }

    public int DeleteWeek(Week week)
    {
      // Delete a week.
      return database.Delete(week);
    }

  }
}
