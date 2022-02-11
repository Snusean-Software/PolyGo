using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;

using PolyGo.Shells;
using PolyGo.Models;


namespace PolyGo.SupportFuncs
{
	internal class InSetupSupportFuncs
	{
		public static void GoToMainShell(User user)
		{
			if (user.Language == null) user.Language = "ru-Ru";

			FileStream fs = File.OpenWrite(App.AccountFilePath);
			StreamWriter sw = new StreamWriter(fs);

		  sw.WriteLine("Language: {0}", user.Language);
			if(user.Name != null) sw.WriteLine("Name: {0}", user.Name);
			if (user.IsStudent != null)
			{
				sw.WriteLine("User is student: {0}", user.IsStudent);
				if ((bool)user.IsStudent) sw.WriteLine("Group number: {0}", user.GroupNum);
			}
			sw.Flush();
			fs.Close();

			Shell.Current.IsVisible = false;
			App.Current.MainPage = new AppShell();
		}
	}
}
