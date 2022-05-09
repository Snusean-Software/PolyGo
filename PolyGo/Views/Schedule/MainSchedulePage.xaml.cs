using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System;

using PolyGo.SupportFuncs;
using PolyGo.Models.Schedule;
using System.Collections.Generic;

namespace PolyGo.Views.Schedule
{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainSchedulePage : ContentPage
	{
		private IList<Root> ScheduleData = new List<Root>();
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

			var str_weekStart = defineWeekForDay(date_Picker.Date);
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

		/// <summary>
		/// Returns start of week which day belongs to
		/// </summary>
		/// <param name="day"></param>
		/// <returns>Start day of week</returns>
		/// 
		private string dateTimeToString(DateTime day)
		{
			var result = day.Year.ToString() + '.'; //+ day.Month.ToString() + '.' + day.Day.ToString();
			if (day.Month / 10 >= 1) result += day.Month.ToString() + '.';
			else result += '0' + day.Month.ToString() + '.';

			if (day.Day / 10 >= 1) result += day.Day.ToString();
			else result += '0' + day.Day.ToString();

			return result;
		}
		private string defineWeekForDay(DateTime day)
		{
			switch (day.DayOfWeek)
			{
				case DayOfWeek.Monday:
					return dateTimeToString(day);
				case DayOfWeek.Tuesday:
					return dateTimeToString(day.AddDays(-1));
				case DayOfWeek.Wednesday:
					return dateTimeToString(day.AddDays(-2));
				case DayOfWeek.Thursday:
					return dateTimeToString(day.AddDays(-3));
				case DayOfWeek.Friday:
					return dateTimeToString(day.AddDays(-4));
				case DayOfWeek.Saturday:
					return dateTimeToString(day.AddDays(-5));
				default:
					return dateTimeToString(day.AddDays(-6));
			}
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
						var dayDate = date_Picker.Date.ToShortDateString().Replace('.', '-'); //dd-mm-yyyy, need yyyy-mm-dd
						dayDate = dayDate.Substring(6) + dayDate.Substring(2, 3) + '-' + dayDate.Substring(0, 2);
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

			if (Connectivity.NetworkAccess != NetworkAccess.Internet)
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
					var dayDate = date_Picker.Date.ToShortDateString().Replace('.', '-'); //dd-mm-yyyy, need yyyy-mm-dd
					dayDate = dayDate.Substring(6) + dayDate.Substring(2, 3) + '-' + dayDate.Substring(0, 2);
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

			var networkAccess = Connectivity.NetworkAccess;
			if (App.SchdlDatabase.Empty)
			{
				switch (networkAccess)
				{
					case NetworkAccess.Internet:
						{
							//Save current and two next weeks
							var currentWeek = await ScheduleSupportFuncs.ParseWeek(Constants.RefToSchedule);
							await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(currentWeek.week, 1));
							await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(currentWeek.week, 1));
							break;
						}
					default:
						{
							//Tell about error
							break;
						}
				}
			}

			var wrongFormatDate = date_Picker.Date.ToShortDateString(); //dd.mm.yyyy, need yyyy.mm.dd
			var currentWeekDate = wrongFormatDate.Substring(6) + wrongFormatDate.Substring(2, 3) + '-' + wrongFormatDate.Substring(0, 2);
			App.SchdlDatabase.ClearOldWeeks(currentWeekDate);

			ScheduleData = App.SchdlDatabase.GetDataForSchedule();

			var str_weekStart = defineWeekForDay(date_Picker.Date);
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