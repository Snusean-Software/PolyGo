using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using PolyGo.SupportFuncs;
using PolyGo.Models.Schedule;

namespace PolyGo.Views.Schedule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainSchedulePage : ContentPage
	{
		public MainSchedulePage()
		{
			InitializeComponent();
		}

		private async void ParseAdjacentWeeksAsync(Week current)
		{
			await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(current, -1));

			await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(current, 2));

			await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(current, 1));
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			Week current = new Week();

			if (App.Database.Empty)
			{
				var networkAccess = Connectivity.NetworkAccess;
				switch (networkAccess)
				{
					case NetworkAccess.Internet:
					{
						current = await ScheduleSupportFuncs.ParseWeek(Constants.RefToSchedule);

						await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(current, -1));

						await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(current, 2));

						await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(current, 1));

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

			collectionViewDays.ItemsSource = data;
		}
	}
}
