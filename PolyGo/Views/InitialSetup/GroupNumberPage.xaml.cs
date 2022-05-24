using PolyGo.SupportFuncs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PolyGo.Views.InitialSetup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupNumberPage : ContentPage
	{
		public GroupNumberPage()
		{
			InitializeComponent();
			content_page.BackgroundImageSource = ImageSource.FromResource("PolyGo.Resources.SetUp.second_background_image.png", GetType().Assembly);
		}

		private void checkIfGroup(object sender, EventArgs e)
		{

		}
		private async void isReturnButtonClicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync($"{nameof(WelcomePage)}?");
		}
		private void groupChosen(object sender, EventArgs e)
		{
			App.user.GroupNum = entry.Text;
			InSetupSupportFuncs.GoToMainShell(App.user);
		}
		
		//public class noLineEntry : Entry
		//{

		//	public noLineEntry()
		//	{
		//		this.Control.SetBackground(null);
		//	}
		//}
	}
}