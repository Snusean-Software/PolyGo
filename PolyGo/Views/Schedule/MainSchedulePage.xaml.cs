using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.SupportFuncs;
using PolyGo.Models.Schedule;
using Xamarin.Essentials;
using System.Linq;

namespace PolyGo.Views.Schedule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainSchedulePage : ContentPage
	{
		public MainSchedulePage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (App.Database.Empty)
			{
				var networkAccess = Connectivity.NetworkAccess;
				switch (networkAccess)
				{
					case NetworkAccess.Internet:
					{
						Week currentWeek = ScheduleSupportFuncs.ParseWeek(Constants.RefToSchedule);
						App.Database.SaveWeek(currentWeek);

						App.Database.SaveWeek(ScheduleSupportFuncs.ParseWeek(
						ScheduleSupportFuncs.GetWeekURL(ScheduleSupportFuncs.ChangeWeek(currentWeek.Start, -1))));

						App.Database.SaveWeek(ScheduleSupportFuncs.ParseWeek(
						ScheduleSupportFuncs.GetWeekURL(ScheduleSupportFuncs.ChangeWeek(currentWeek.Start, 2))));

						App.Database.SaveWeek(ScheduleSupportFuncs.ParseWeek(
						ScheduleSupportFuncs.GetWeekURL(ScheduleSupportFuncs.ChangeWeek(currentWeek.Start, 1))));
						break;
					}
					default:
					{
						LabelNetworkError.IsVisible = true;
						break;
					}
				}
			}

			var data = App.Database.GetAllData();

			collectionViewDays.ItemsSource = data[1].Days;
		}
	}
}