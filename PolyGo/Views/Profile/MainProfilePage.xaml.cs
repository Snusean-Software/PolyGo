using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.Models;
using PolyGo.SupportFuncs;

namespace PolyGo.Views.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainProfilePage : ContentPage
	{
		public MainProfilePage()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			User user = new User();
			MainAppSupportFuncs.ParseAccFile(App.AccountFilePath, ref user);

			BindingContext = user;
		}
	}
}