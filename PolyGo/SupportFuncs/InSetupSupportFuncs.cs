using System.IO;
using Xamarin.Forms;

using PolyGo.Shells;
using PolyGo.Models;


namespace PolyGo.SupportFuncs
{
	internal static class InSetupSupportFuncs
	{
		public static void GoToMainShell(User user)
		{
			FileStream fs = File.OpenWrite(Constants.AccountPath);
			StreamWriter sw = new StreamWriter(fs);

			if (user.Language == null) user.Language = "ru-Ru";
			sw.WriteLine("Language: {0}", user.Language);

			if(user.Name != null) sw.WriteLine("Name: {0}", user.Name);
			if (user.IsStudent != null)
			{
				sw.WriteLine("IsStudent: {0}", user.IsStudent);
				if ((bool)user.IsStudent 
					&& user.GroupNum != null) sw.WriteLine("GroupNum: {0}", user.GroupNum);
			}

			sw.Flush();
			fs.Close();

			Shell.Current.IsVisible = false;
			App.Current.MainPage = new AppShell();
		}
	}
}
