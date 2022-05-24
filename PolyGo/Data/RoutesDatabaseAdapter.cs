using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using PolyGo.Models.Navigation.Campus;

namespace PolyGo.Data
{
	public class RoutesDatabaseAdapter
	{
		readonly SQLiteConnection database;

		public RoutesDatabaseAdapter()
		{
			var assembly = GetType().GetTypeInfo().Assembly;

			using (var stream = assembly.GetManifestResourceStream("PolyGo.Resources.schedule.Data.RouteDatabase.db3"))
			{
				FileStream fs = File.OpenWrite(Constants.RoutsDatabasePath);

				stream.CopyTo(fs);

				fs.Close();
			}

			database = new SQLiteConnection(Constants.RoutsDatabasePath);
		}

		public List<Place> Places
		{
			get
			{
				List<Place> plcs = new List<Place>();
				foreach (var plc in database.Table<Place>())
				{
					plcs.Add(plc);
				}

				return plcs;
			}
		}

		public List<Route> Routes
		{
			get
			{
				List<Route> rts = new List<Route>();
				foreach (var rt in database.Table<Route>())
				{
					rts.Add(rt);
				}

				return rts;
			}
		}
	}
}
