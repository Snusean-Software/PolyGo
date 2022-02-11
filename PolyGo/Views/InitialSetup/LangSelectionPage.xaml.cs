using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.Models;
using PolyGo.SupportFuncs;

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
			await Shell.Current.GoToAsync($"{nameof(IsStudentPage)}");
		}
	  void OnDoLaterButtonClicked(object sender, EventArgs e)
		{
			InSetupSupportFuncs.GoToMainShell(App.user);
		}
	}
}