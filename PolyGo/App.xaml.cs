using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.Shells;
using PolyGo.Models;

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
			FileInfo accInf = new FileInfo(AccountFilePath);
			File.Delete(AccountFilePath);
			if (!accInf.Exists)
			{
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
