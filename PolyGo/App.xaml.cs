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
		public static string AccountFilePath { get; private set; }
		internal static User user = new User();
		public App()
		{
			InitializeComponent();
			AccountFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
							 $"account.txt");

			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
			//File.Delete(AccountFilePath);
			FileInfo accInf = new FileInfo(AccountFilePath);
			if (!accInf.Exists)
			{
				AppResources.Culture = CultureInfo.InstalledUICulture;
				FileStream fs = File.Create(AccountFilePath);
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
