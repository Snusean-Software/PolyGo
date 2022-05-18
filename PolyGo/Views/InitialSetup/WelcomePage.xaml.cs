using PolyGo.Resx;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PolyGo.Views.InitialSetup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomePage : ContentPage
	{
		public WelcomePage()
		{
			InitializeComponent();
			content_page.BackgroundImageSource = ImageSource.FromResource("PolyGo.Resources.SetUp.first_background_image.png", GetType().Assembly);
		}

		private async void choice_Student(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync($"{nameof(GroupNumberPage)}");
		}
		private async void choice_Teacher(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync($"{nameof(GroupNumberPage)}");
		}
		private void change_Lan(object sender, EventArgs e)
		{
			if(App.user.Language == "ru-RU")
			{
				App.user.Language = "en-US";
				AppResources.Culture = new CultureInfo("en-US");
			}
			else
			{
				App.user.Language = "ru-RU";
				AppResources.Culture = new CultureInfo("ru-RU");
			}
		}
	}
}