using System;
using System.Collections.Generic;
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
		private IList<Root> ScheduleData = new List<Root>();

		//Property for network access check
		NetworkAccess NA
		{
			get
			{
				return Connectivity.NetworkAccess;
			}
		}
		public MainSchedulePage()
		{
			InitializeComponent();
			nolessons_Image.Source = ImageSource.FromResource("PolyGo.Resources.schedule.no_lessons_image.png", GetType().Assembly);
			shedule_polygon_image_1.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_2.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_3.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_4.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_5.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_6.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_7.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
		}

		private void configurePolygons()
		{
			switch (date_Picker.Date.DayOfWeek)
			{
				case DayOfWeek.Monday:
					shedule_polygon_image_1.IsVisible = true;
					{
						mon_date.Text = date_Picker.Date.Day.ToString();
						tue_date.Text = date_Picker.Date.AddDays(1).Date.Day.ToString();
						wed_date.Text = date_Picker.Date.AddDays(2).Date.Day.ToString();
						thu_date.Text = date_Picker.Date.AddDays(3).Date.Day.ToString();
						fri_date.Text = date_Picker.Date.AddDays(4).Date.Day.ToString();
						sat_date.Text = date_Picker.Date.AddDays(5).Date.Day.ToString();
						sun_date.Text = date_Picker.Date.AddDays(6).Date.Day.ToString();
					}
					break;

				case DayOfWeek.Tuesday:
					shedule_polygon_image_2.IsVisible = true;
					{
						mon_date.Text = date_Picker.Date.AddDays(-1).Date.Day.ToString();
						tue_date.Text = date_Picker.Date.Day.ToString();
						wed_date.Text = date_Picker.Date.AddDays(1).Date.Day.ToString();
						thu_date.Text = date_Picker.Date.AddDays(2).Date.Day.ToString();
						fri_date.Text = date_Picker.Date.AddDays(3).Date.Day.ToString();
						sat_date.Text = date_Picker.Date.AddDays(4).Date.Day.ToString();
						sun_date.Text = date_Picker.Date.AddDays(5).Date.Day.ToString();
					}
					break;

				case DayOfWeek.Wednesday:
					shedule_polygon_image_3.IsVisible = true;
					{
						mon_date.Text = date_Picker.Date.AddDays(-2).Date.Day.ToString();
						tue_date.Text = date_Picker.Date.AddDays(-1).Date.Day.ToString();
						wed_date.Text = date_Picker.Date.Day.ToString();
						thu_date.Text = date_Picker.Date.AddDays(1).Date.Day.ToString();
						fri_date.Text = date_Picker.Date.AddDays(2).Date.Day.ToString();
						sat_date.Text = date_Picker.Date.AddDays(3).Date.Day.ToString();
						sun_date.Text = date_Picker.Date.AddDays(4).Date.Day.ToString();
					}
					break;

				case DayOfWeek.Thursday:
					shedule_polygon_image_4.IsVisible = true;
					{
						mon_date.Text = date_Picker.Date.AddDays(-3).Date.Day.ToString();
						tue_date.Text = date_Picker.Date.AddDays(-2).Date.Day.ToString();
						wed_date.Text = date_Picker.Date.AddDays(-1).Date.Day.ToString();
						thu_date.Text = date_Picker.Date.Day.ToString();
						fri_date.Text = date_Picker.Date.AddDays(1).Date.Day.ToString();
						sat_date.Text = date_Picker.Date.AddDays(2).Date.Day.ToString();
						sun_date.Text = date_Picker.Date.AddDays(3).Date.Day.ToString();
					}
					break;

				case DayOfWeek.Friday:
					shedule_polygon_image_5.IsVisible = true;
					{
						mon_date.Text = date_Picker.Date.AddDays(-4).Date.Day.ToString();
						tue_date.Text = date_Picker.Date.AddDays(-3).Date.Day.ToString();
						wed_date.Text = date_Picker.Date.AddDays(-2).Date.Day.ToString();
						thu_date.Text = date_Picker.Date.AddDays(-1).Date.Day.ToString();
						fri_date.Text = date_Picker.Date.Day.ToString();
						sat_date.Text = date_Picker.Date.AddDays(1).Date.Day.ToString();
						sun_date.Text = date_Picker.Date.AddDays(1).Date.Day.ToString();
					}
					break;

				case DayOfWeek.Saturday:
					shedule_polygon_image_6.IsVisible = true;
					{
						mon_date.Text = date_Picker.Date.AddDays(-5).Date.Day.ToString();
						tue_date.Text = date_Picker.Date.AddDays(-4).Date.Day.ToString();
						wed_date.Text = date_Picker.Date.AddDays(-3).Date.Day.ToString();
						thu_date.Text = date_Picker.Date.AddDays(-2).Date.Day.ToString();
						fri_date.Text = date_Picker.Date.AddDays(-1).Date.Day.ToString();
						sat_date.Text = date_Picker.Date.Day.ToString();
						sun_date.Text = date_Picker.Date.AddDays(+1).Date.Day.ToString();
					}
					break;

				case DayOfWeek.Sunday:
					shedule_polygon_image_7.IsVisible = true;
					{
						mon_date.Text = date_Picker.Date.AddDays(-6).Date.Day.ToString();
						tue_date.Text = date_Picker.Date.AddDays(-5).Date.Day.ToString();
						wed_date.Text = date_Picker.Date.AddDays(-4).Date.Day.ToString();
						thu_date.Text = date_Picker.Date.AddDays(-3).Date.Day.ToString();
						fri_date.Text = date_Picker.Date.AddDays(-2).Date.Day.ToString();
						sat_date.Text = date_Picker.Date.AddDays(-1).Date.Day.ToString();
						sun_date.Text = date_Picker.Date.Day.ToString();
					}
					break;
			}
		}
		private void onDateSelected(object sender, EventArgs e)
		{
			myColl.ItemsSource = null;
			clearShedulePolygons();
			configurePolygons();

			var str_weekStart = ScheduleSupportFuncs.defineWeekForDay(date_Picker.Date);
			loadSchedule(str_weekStart);
		}
		private void clearShedulePolygons()
		{
			shedule_polygon_image_1.IsVisible = false;
			shedule_polygon_image_2.IsVisible = false;
			shedule_polygon_image_3.IsVisible = false;
			shedule_polygon_image_4.IsVisible = false;
			shedule_polygon_image_5.IsVisible = false;
			shedule_polygon_image_6.IsVisible = false;
			shedule_polygon_image_7.IsVisible = false;
		}
		private void onGroupsOfLessonTapped(object sender, EventArgs e)
		{

		}
		private async void loadSchedule(string str_weekStart)
		{
			bool flag_ScheduleNotLoad = true;
			foreach (var rt in ScheduleData)
			{
				if (rt.week_date_start == str_weekStart)
				{
					foreach (var day in rt.days)
					{
						var dayDate = ScheduleSupportFuncs.dateTimeToString(date_Picker.Date).Replace('.', '-'); //yyyy-mm-dd
						if (day.date == dayDate)
						{
							myColl.ItemsSource = day.lessons;
							break;
						}
					}

					flag_ScheduleNotLoad = false;
					break;
				}
			}

			if (NA != NetworkAccess.Internet)
			{
				//Tell about error
				return;
			}

			Root newWeek;
			if (flag_ScheduleNotLoad)
			{
				newWeek = await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.GetWeekURL(str_weekStart));
				ScheduleData.Add(newWeek);
				foreach (var day in newWeek.days)
				{
					var dayDate = ScheduleSupportFuncs.dateTimeToString(date_Picker.Date).Replace('.', '-'); //yyyy-mm-dd
					if (day.date == dayDate)
					{
						myColl.ItemsSource = day.lessons;
						break;
					}
				}
			}
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			configurePolygons();

			switch (NA)
			{
				case NetworkAccess.Internet:
					{
						//Save or update current and two next weeks
						var currentWeek = await ScheduleSupportFuncs.ParseWeek(Constants.RefToSchedule);
						await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(currentWeek.week, 1));
						await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(currentWeek.week, 1));
						break;
					}
				default:
					{
						if (App.SchdlDatabase.Empty)
						{
							//Tell about error
						}
						break;
					}
			}

			var currentWeekDate = ScheduleSupportFuncs.dateTimeToString(date_Picker.Date); ///yyyy-mm-dd
			App.SchdlDatabase.ClearOldWeeks(currentWeekDate);

			ScheduleData = App.SchdlDatabase.GetAllData();

			var str_weekStart = ScheduleSupportFuncs.defineWeekForDay(date_Picker.Date);
			loadSchedule(str_weekStart);
		}
	}
	public class ButtonLink : ImageButton
	{
		public string Link
		{
			get { return (string)GetValue(LinkProperty); }
			set { SetValue(LinkProperty, value); }
		}

		public static readonly BindableProperty LinkProperty =
			BindableProperty.Create("Link", typeof(string), typeof(ButtonLink), null);
		public ButtonLink()
		{
			Source = ImageSource.FromResource("PolyGo.Resources.schedule.sdo_link_button.png", GetType().Assembly);
			Clicked += async (sender, e) =>
			{
				try
				{
					await Browser.OpenAsync(new Uri(Link.ToString()));
				}
				catch (Exception) { }
			};
		}
	}
}