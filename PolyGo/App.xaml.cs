using System;
using System.IO;
using System.Globalization;
using Xamarin.Forms;

using PolyGo.Shells;
using PolyGo.Models;
using PolyGo.Resx;

namespace PolyGo
{
	public partial class App : Application
	{
		internal static User user = new User();
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
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
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
