using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using PolyGo.SupportFuncs;
using PolyGo.Models.Schedule;

namespace PolyGo.Views.Schedule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainSchedulePage : TabbedPage
	{
		public MainSchedulePage()
		{
			InitializeComponent();
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			Week current = new Week();

			var networkAccess = Connectivity.NetworkAccess;
			if (App.Database.Empty)
			{
				switch (networkAccess)
				{
					case NetworkAccess.Internet:
						{
							current = await ScheduleSupportFuncs.ParseWeek(Constants.RefToSchedule);

							//await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(current, -1));

							//await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(current, 2));

							//await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(current, 1));

							break;
						}
					default:
						{

							break;
						}
				}
			}
			else // if database not empty
			{

				switch (networkAccess)
				{
					case NetworkAccess.Internet:
						{
							current = await ScheduleSupportFuncs.ParseWeek(Constants.RefToSchedule);

							break;
						}
					default:
						{

							break;
						}
				}

				//App.Database.ClearOldWeeks(); current data
			}
			//App.Database.ClearFacultyGroups();
			//ScheduleSupportFuncs.ParceFacultyNumbers();

			var data = App.Database.GetDataForSchedule();
			//var groups = App.Database.GetFacultyGroups();
			int x = 0;
			++x;

			//collectionViewDays.ItemsSource = data;
		}
	}
}
