using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using PolyGo.Models.Schedule;

namespace PolyGo.Data
{
	public class FacultyGroupsDatabaseAdapter
	{
		 readonly SQLiteConnection database;

		public FacultyGroupsDatabaseAdapter()
		{
			var assembly = GetType().GetTypeInfo().Assembly;

			using (var stream = assembly.GetManifestResourceStream("PolyGo.Resources.schedule.Data.FacultyGroups.db3"))
			{
				FileStream fs = File.OpenWrite(Constants.FacultyGroupsDatabasePath);

				stream.CopyTo(fs);

				fs.Close();
			}

			database = new SQLiteConnection(Constants.FacultyGroupsDatabasePath);
		}

		public List<FacultyGroup> FacultyGroups 
		{ 
			get
			{
				List<FacultyGroup> fgs = new List<FacultyGroup>();
				foreach (var fg in database.Table<FacultyGroup>())
				{
					fgs.Add(fg);
				}

				return fgs;
			}
		}
	}
}
