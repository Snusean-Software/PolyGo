using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.Data;
using PolyGo.Models.Schedule;
using PolyGo.SupportFuncs;
using System.Collections.Generic;

namespace PolyGo.Views.InitialSetup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupNumberPage : ContentPage
	{
		private List<FacultyGroup> Groups = App.FgDatabase.FacultyGroups;
		public GroupNumberPage()
		{
			InitializeComponent();
			content_page.BackgroundImageSource = ImageSource.FromResource("PolyGo.Resources.SetUp.second_background_image.png", GetType().Assembly);
		}

		private async void isReturnButtonClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync($"{nameof(WelcomePage)}?");
		}

		private void groupChosen(object sender, EventArgs e)
		{
			App.user.GroupNum = entry.Text;
			App.user.LinkSchedule = Groups.Find(x => x.Name == entry.Text).URL;
			InSetupSupportFuncs.GoToMainShell(App.user);
		}

		private void GroupNumberChange(object sender, EventArgs e)
		{
			if (entry.Text.Length > 0) groupsFrame.IsVisible = true;
			List<FacultyGroup> grps = new List<FacultyGroup>();
			int counter = 0;
			foreach (var gr in Groups)
			{
				if (gr.Name.StartsWith(entry.Text))
				{
					grps.Add(gr);
					++counter;
					if (counter > 10) break;
				}
			}
			groups.ItemsSource = grps;
			if(grps.Count == 0) groupsFrame.IsVisible = false;
		}

		private void onGroupTapped(object sender, EventArgs e)
		{
			var label = sender as Label;
			entry.Text = label.Text;
			groupsFrame.IsVisible = false;
		}
	}
}