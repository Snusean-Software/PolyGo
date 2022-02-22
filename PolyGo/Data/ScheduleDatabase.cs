using System.Threading.Tasks;
using System.Collections.Generic;
using SQLite;

using PolyGo.Models.Schedule;

namespace PolyGo.Data
{
	internal class ScheduleDatabase
	{
    readonly SQLiteAsyncConnection database;

    public ScheduleDatabase()
    {
      database = new SQLiteAsyncConnection(Constants.DatabasePath,Constants.Flags);
      database.CreateTableAsync<Week>().Wait();
    }

    public Task<List<Week>> GetWeeksAsync()
    {
      //Get all schedule.
      return database.Table<Week>().ToListAsync();
    }

    public Task<Week> GetWeekAsync(int id)
    {
      // Get a specific week.
      return database.Table<Week>()
                      .Where(i => i.ID == id)
                      .FirstOrDefaultAsync();
    }

    public Task<int> SaveWeekAsync(Week week)
    {
      if (week.ID != 0)
      {
        // Update an existing week.
        return database.UpdateAsync(week);
      }
      else
      {
        // Save a new week.
        return database.InsertAsync(week);
      }
    }

    public Task<int> DeleteWeekAsync(Week week)
    {
      // Delete a week.
      return database.DeleteAsync(week);
    }

  }
}
