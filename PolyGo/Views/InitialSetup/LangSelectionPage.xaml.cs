using System;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.SupportFuncs;
using PolyGo.Resx;

namespace PolyGo.Views.InitialSetup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LangSelectionPage : ContentPage
	{
		public LangSelectionPage()
		{
			InitializeComponent();
		}
		async void OnNextButtonClicked(object sender, EventArgs e)
		{
			if(rbRu.IsChecked)
			{
				App.user.Language = "ru-RU";
				AppResources.Culture = new CultureInfo("ru-RU");
				await Shell.Current.GoToAsync($"{nameof(IsStudentPage)}");
			}
			else if(rbEn.IsChecked)
			{
				App.user.Language = "en-US";
				AppResources.Culture = new CultureInfo("en-US");
				await Shell.Current.GoToAsync($"{nameof(IsStudentPage)}");
			}
			else
			{
				await DisplayAlert(AppResources.Notification, AppResources.NotifTextLanguage, AppResources.NotifYes);
			}
		}
	  void OnDoLaterButtonClicked(object sender, EventArgs e)
		{
			InSetupSupportFuncs.GoToMainShell(App.user);
		}
	}
}