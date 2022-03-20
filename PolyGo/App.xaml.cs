using System;
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

	  static ScheduleDatabase database;

		// Create the database connection as a singleton.
	  internal static ScheduleDatabase Database
		{
			get
			{
				if (database == null)
				{
					database = new ScheduleDatabase();
				}
				return database;
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
				MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.Black);
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
