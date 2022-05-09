using System;
using System.Collections.Generic;

using SQLite;

using PolyGo.Models.Schedule;
using PolyGo.SupportFuncs;

namespace PolyGo.Data
{
  public class ScheduleDatabase
  {
    readonly SQLiteConnection database;
    public ScheduleDatabase()
    {
      database = new SQLiteConnection(Constants.ScheduleDatabasePath);

      database.CreateTable<Root>();
      database.CreateTable<Day>();
      database.CreateTable<Lesson>();
      database.CreateTable<Group>();
      database.CreateTable<Teacher>();
      database.CreateTable<Auditory>();
      database.CreateTable<FacultyGroup>();
    }

    /// <summary>
    /// If table Root is empty
    /// </summary>
    public bool Empty
    {
      get
      {
        return database.Table<Root>().Count() == 0;
      }
    }

    /// <summary>
    /// Save Root to database and creates ID
    /// connections between tables
    /// </summary>
    /// <param name="rt">Object Root which will be written to the database</param>
    /// <returns>The number of rows inserted or updated in Root table</returns>
    public int SaveRoot(Root rt)
    {
      if (rt.ID != 0)
      {
        var result = database.Update(rt);
        foreach (var day in rt.days)
        {
          day.IjRootID = rt.ID;
          database.Update(day);
          foreach (var les in day.lessons)
          {
            les.IjDaysID = day.ID;
            database.Update(les);

            foreach (var gr in les.groups)
            {
              gr.IjLessonID = les.ID;
              database.Update(gr);
            }

            try
            {
              foreach (var ad in les.auditories)
              {
                ad.IjLessonID = les.ID;
                database.Update(ad);
              }
            }
            catch (Exception) { }

            try
            {
              foreach (var teacher in les.teachers)
              {
                teacher.IjLessonID = les.ID;
                database.Update(teacher);
              }
            }
            catch (Exception) { }
          }
        }
        return result;
      }
      else
      {
        var result = database.Insert(rt);
        foreach (var day in rt.days)
        {
          day.IjRootID = rt.ID;
          database.Insert(day);
          foreach (var les in day.lessons)
          {
            les.IjDaysID = day.ID;
            database.Insert(les);

            foreach (var gr in les.groups)
            {
              gr.IjLessonID = les.ID;
              database.Insert(gr);
            }

            try
            {
              foreach (var ad in les.auditories)
              {
                ad.IjLessonID = les.ID;
                database.Insert(ad);
              }
            }
            catch (Exception) { }

            try
            {
              foreach (var teacher in les.teachers)
              {
                teacher.IjLessonID = les.ID;
                database.Insert(teacher);
              }
            }
            catch (Exception) { }
          }
        }
        return result;
      }
    }
    public int SaveFacultyGroup(FacultyGroup facultyGroup)
    {
      if (facultyGroup.ID != 0)
      {
        return database.Update(facultyGroup);
      }
      else
      {
        return database.Insert(facultyGroup);
      }
    }
    private List<Day> getAllDaysByID(int IJid)
    {
      List<Day> days = new List<Day>();
      foreach (var day in database.Table<Day>())
      {
        if (day.IjRootID == IJid) days.Add(day);
      }
      return days;
    }
    private List<Lesson> getAllLessonsByID(int IJid)
    {
      List<Lesson> lessons = new List<Lesson>();
      foreach (var lesson in database.Table<Lesson>())
      {
        if (lesson.IjDaysID == IJid) lessons.Add(lesson);
      }
      return lessons;
    }
    private List<Teacher> getAllTeachersByID(int IJid)
    {
      List<Teacher> teachers = new List<Teacher>();
      foreach (var teacher in database.Table<Teacher>())
      {
        if (teacher.IjLessonID == IJid) teachers.Add(teacher);
      }
      return teachers;
    }
    private List<Auditory> getAllAuditoriesByID(int IJid)
    {
      List<Auditory> auds = new List<Auditory>();
      foreach (var au in database.Table<Auditory>())
      {
        if (au.IjLessonID == IJid) auds.Add(au);
      }
      return auds;
    }
    private List<Group> getAllGroupsByID(int IJid)
    {
      List<Group> groups = new List<Group>();
      foreach (var group in database.Table<Group>())
      {
        if (group.IjLessonID == IJid) groups.Add(group);
      }
      return groups;
    }
    private Root findRootByStartDay(string sDay)
    {
      foreach (var rt in database.Table<Root>())
      {
        if (rt.week_date_start == sDay) return rt;
      }
      return null;
    }

    /// <summary>
    /// Returns all data from all tables
    /// </summary>
    /// <returns>List of Roots from database</returns>
    public List<Root> GetAllData()
    {
      List<Root> roots = new List<Root>();

      foreach (var rt in database.Table<Root>())
      {
        foreach (var day in getAllDaysByID(rt.ID))
        {
          foreach (var lesson in getAllLessonsByID(day.ID))
          {

            foreach (var gr in getAllGroupsByID(lesson.ID))
            {
              lesson.groups.Add(gr);
            }

            try
            {
              foreach (var au in getAllAuditoriesByID(lesson.ID))
              {
                lesson.auditories.Add(au);
              }
            }
            catch (Exception) { }

            try
            {
              foreach (var teacher in getAllTeachersByID(lesson.ID))
              {
                lesson.teachers.Add(teacher);
              }
            }
            catch (Exception) { }

            day.lessons.Add(lesson);
          }
          rt.days.Add(day);
        }
        roots.Add(rt);
      }

      return roots;
    }

    /// <summary>
    /// Returns data from database which will be displayed on the schedule page
    /// </summary>
    /// <returns>List of roots from database without groups lists in Lessons</returns>
    public List<Root> GetDataForSchedule()
    {
      List<Root> roots = new List<Root>();

      foreach (var rt in database.Table<Root>())
      {
        foreach (var day in getAllDaysByID(rt.ID))
        {
          foreach (var lesson in getAllLessonsByID(day.ID))
          {
            try
            {
              foreach (var au in getAllAuditoriesByID(lesson.ID))
              {
                lesson.auditories.Add(au);
              }
            }
            catch (Exception) { }

            try
            {
              foreach (var teacher in getAllTeachersByID(lesson.ID))
              {
                lesson.teachers.Add(teacher);
              }
            }
            catch (Exception) { }

            day.lessons.Add(lesson);
          }
          rt.days.Add(day);
        }
        roots.Add(rt);
      }

      return roots;
    }

    /// <summary>
    /// Returns one Root from database with definite start day of week
    /// </summary>
    /// <param name="sDay">Start day of week</param>
    /// <returns>Root with definite start day of week</returns>
    public Root GetRootByStartDay(string sDay)
    {
      Root rt = findRootByStartDay(sDay);

      foreach (var day in getAllDaysByID(rt.ID))
      {
        foreach (var lesson in getAllLessonsByID(day.ID))
        {

          foreach (var gr in getAllGroupsByID(lesson.ID))
          {
            lesson.groups.Add(gr);
          }

          try
          {
            foreach (var au in getAllAuditoriesByID(lesson.ID))
            {
              lesson.auditories.Add(au);
            }
          }
          catch (Exception) { }

          try
          {
            foreach (var teacher in getAllTeachersByID(lesson.ID))
            {
              lesson.teachers.Add(teacher);
            }
          }
          catch (Exception) { }

          day.lessons.Add(lesson);
        }
        rt.days.Add(day);
      }

      return rt;
    }

    public List<FacultyGroup> GetFacultyGroups()
    {
      List<FacultyGroup> facultyGroups = new List<FacultyGroup>();

      foreach (var fg in database.Table<FacultyGroup>())
      {
        facultyGroups.Add(fg);
      }

      return facultyGroups;
    }

    public void ClearFacultyGroups()
    {
      foreach (var fg in database.Table<FacultyGroup>())
      {
        database.Delete(fg);
      }
    }

    /// <summary>
    /// Delete weeks that were 3 weeks ago and earlier 
    /// </summary>
    /// <param name="start_date">Start day of current week</param>
    public void ClearOldWeeks(string start_date)
    {
      foreach (var rt in database.Table<Root>())
      {
        var start = ScheduleSupportFuncs.getWeekDate(start_date);
        var current = ScheduleSupportFuncs.getWeekDate(rt.week_date_start);
        DateTime dt_start = new DateTime(start.year, start.month, start.day);
        DateTime dt_max_old = dt_start.AddDays(-14);
        DateTime dt_current = new DateTime(current.year, current.month, current.day);
        if (dt_max_old.CompareTo(dt_current) > 0)
        {
          database.Delete(rt);
          foreach (var day in database.Table<Day>())
          {
            if (day.IjRootID == rt.ID)
            {
              database.Delete(day);
              foreach (var lsn in database.Table<Lesson>())
              {
                if (lsn.IjDaysID == day.ID)
                {
                  database.Delete(lsn);
                  foreach (var gr in database.Table<Group>())
                  {
                    if (gr.IjLessonID == lsn.ID) database.Delete(gr);
                  }
                  foreach (var teacher in database.Table<Teacher>())
                  {
                    if (teacher.IjLessonID == lsn.ID) database.Delete(teacher);
                  }
                  foreach (var aud in database.Table<Auditory>())
                  {
                    if (aud.IjLessonID == lsn.ID) database.Delete(aud);
                  }
                }
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Checks if week is in database
    /// </summary>
    /// <param name="start_date"></param>
    /// <returns></returns>
    public bool checkWeekInDB(string start_date)
    {
      foreach (var rt in database.Table<Root>())
      {
        if (start_date == rt.week_date_start)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Updates info about chosen week
    /// </summary>
    /// <param name="root"></param>
    public void updateWeek(Root root)
    {
      foreach (var rt in database.Table<Root>())
      {
        if (root.week_date_start == rt.week_date_start)
        {
          database.Delete(rt);
          database.Insert(rt);
          foreach (var day in database.Table<Day>())
          {
            if (day.IjRootID == rt.ID)
            {
              database.Delete(day);
              database.Insert(day);
              foreach (var lsn in database.Table<Lesson>())
              {
                if (lsn.IjDaysID == day.ID)
                {
                  database.Delete(lsn);
                  database.Insert(lsn);
                  foreach (var gr in database.Table<Group>())
                  {
                    if (gr.IjLessonID == lsn.ID)
                    {
                      database.Delete(gr);
                      database.Insert(gr);
                    }
                  }
                  foreach (var teacher in database.Table<Teacher>())
                  {
                    if (teacher.IjLessonID == lsn.ID)
                    {
                      database.Delete(teacher);
                      database.Insert(teacher);
                    }
                  }
                  foreach (var aud in database.Table<Auditory>())
                  {
                    if (aud.IjLessonID == lsn.ID)
                    {
                      database.Delete(aud);
                      database.Insert(aud);
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}

