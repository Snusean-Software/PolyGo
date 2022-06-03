using System.IO;
using System.Globalization;
using Xamarin.Forms;

using PolyGo.Shells;
using PolyGo.Models;
using PolyGo.Resx;
using PolyGo.Data;

namespace PolyGo
{
	public partial class App : Application
	{
		internal static User user;

	  static ScheduleDatabase scheduleDatabase;
		static MapDatabase mapDatabase;
		static FacultyGroupsDatabaseAdapter facultyGroupsDatabase;
		static RoutesDatabaseAdapter rtsDatabase;

		// Create the database connection as a singleton.
		internal static ScheduleDatabase SchdlDatabase
		{
			get
			{
				if (scheduleDatabase == null)
				{
					scheduleDatabase = new ScheduleDatabase();
				}
				return scheduleDatabase;
			}
		}

		internal static MapDatabase MpDatabase
		{
			get 
			{
				if (mapDatabase == null)
				{
					mapDatabase = new MapDatabase();
				}
				return mapDatabase;
			}
		}

		internal static FacultyGroupsDatabaseAdapter FgDatabase
		{
			get
			{
				if (facultyGroupsDatabase == null)
				{
					facultyGroupsDatabase = new FacultyGroupsDatabaseAdapter();
				}
				return facultyGroupsDatabase;
			}
		}

		internal static RoutesDatabaseAdapter RtsDatabase
		{
			get
			{
				if (rtsDatabase == null)
				{
					rtsDatabase = new RoutesDatabaseAdapter();
				}
				return rtsDatabase;
			}
		}

		public App()
		{
			InitializeComponent();

			user = new User();
		}

		protected override void OnStart()
		{
			FileInfo accInf = new FileInfo(Constants.AccountPath);
			if (!accInf.Exists)
			{
				AppResources.Culture = CultureInfo.InstalledUICulture;
				FileStream fs = File.Create(Constants.AccountPath);
				fs.Close();
				MainPage = new InitialSetupShell();
			}
			else MainPage = new AppShell();
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
