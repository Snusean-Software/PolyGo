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
		private int nowDayOfWheek = 0;

		//Property for network access check
		private NetworkAccess NA
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
			shedule_polygon_image_mon.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_tue.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_wed.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_thu.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_fri.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_sat.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			shedule_polygon_image_sun.Source = ImageSource.FromResource("PolyGo.Resources.schedule.shedule_polygon_image.png", GetType().Assembly);
			calculateNowDayOfWheek();

			parseSchedule();
		}
		private async void parseSchedule()
		{
			switch (NA)
			{
				case NetworkAccess.Internet:
					{
						//Save current and next week
						var currentWeek = await ScheduleSupportFuncs.ParseWeek(Constants.RefToSchedule);
						await ScheduleSupportFuncs.ParseWeek(ScheduleSupportFuncs.ChangeWeekUrl(currentWeek.week, 1));
						break;
					}
				default:
					{
						//Tell about error or not
						break;
					}
			}
		}
		private void calculateNowDayOfWheek()
		{
			switch (date_Picker.Date.DayOfWeek)
			{
				case DayOfWeek.Monday:
					nowDayOfWheek = 0;
					break;

				case DayOfWeek.Tuesday:
					nowDayOfWheek = 1;
					break;

				case DayOfWeek.Wednesday:
					nowDayOfWheek = 2;
					break;

				case DayOfWeek.Thursday:
					nowDayOfWheek = 3;
					break;

				case DayOfWeek.Friday:
					nowDayOfWheek = 4;
					break;

				case DayOfWeek.Saturday:
					nowDayOfWheek = 5;
					break;

				case DayOfWeek.Sunday:
					nowDayOfWheek = 6;
					break;
			}
		}
		private void changePolygonPosition(int newDayOfWheek)
		{
			int daysDifference = newDayOfWheek - nowDayOfWheek;
			if (daysDifference != 0)
			{
				clearShedulePolygons();
				switch (newDayOfWheek)
				{
					case 0:
						shedule_polygon_image_mon.IsVisible = true;
						date_Picker.Date = date_Picker.Date.AddDays(daysDifference);
						break;

					case 1:
						shedule_polygon_image_tue.IsVisible = true;
						date_Picker.Date = date_Picker.Date.AddDays(daysDifference);
						break;

					case 2:
						shedule_polygon_image_wed.IsVisible = true;
						date_Picker.Date = date_Picker.Date.AddDays(daysDifference);
						break;

					case 3:
						shedule_polygon_image_thu.IsVisible = true;
						date_Picker.Date = date_Picker.Date.AddDays(daysDifference);
						break;

					case 4:
						shedule_polygon_image_fri.IsVisible = true;
						date_Picker.Date = date_Picker.Date.AddDays(daysDifference);
						break;

					case 5:
						shedule_polygon_image_sat.IsVisible = true;
						date_Picker.Date = date_Picker.Date.AddDays(daysDifference);
						break;

					case 6:
						shedule_polygon_image_sun.IsVisible = true;
						date_Picker.Date = date_Picker.Date.AddDays(daysDifference);
						break;
				}
			}
		}
		private void onMonDateTapped(object sender, EventArgs e)
		{
			changePolygonPosition(0);
			var str_weekStart = defineWeekForDay(date_Picker.Date);
			loadSchedule(str_weekStart);
			calculateNowDayOfWheek();
		}
		private void onTueDateTapped(object sender, EventArgs e)
		{
			changePolygonPosition(1);
			var str_weekStart = defineWeekForDay(date_Picker.Date);
			loadSchedule(str_weekStart);
			calculateNowDayOfWheek();
		}
		private void onWedDateTapped(object sender, EventArgs e)
		{
			changePolygonPosition(2);
			var str_weekStart = defineWeekForDay(date_Picker.Date);
			loadSchedule(str_weekStart);
			calculateNowDayOfWheek();
		}
		private void onThuDateTapped(object sender, EventArgs e)
		{
			changePolygonPosition(3);
			var str_weekStart = defineWeekForDay(date_Picker.Date);
			loadSchedule(str_weekStart);
			calculateNowDayOfWheek();
		}
		private void onFriDateTapped(object sender, EventArgs e)
		{
			changePolygonPosition(4);
			var str_weekStart = defineWeekForDay(date_Picker.Date);
			loadSchedule(str_weekStart);
			calculateNowDayOfWheek();
		}
		private void onSatDateTapped(object sender, EventArgs e)
		{
			changePolygonPosition(5);
			var str_weekStart = defineWeekForDay(date_Picker.Date);
			loadSchedule(str_weekStart);
			calculateNowDayOfWheek();
		}
		private void onSunDateTapped(object sender, EventArgs e)
		{
			changePolygonPosition(6);
			var str_weekStart = defineWeekForDay(date_Picker.Date);
			loadSchedule(str_weekStart);
			calculateNowDayOfWheek();
		}
		private void configurePolygons()
		{
			switch (date_Picker.Date.DayOfWeek)
			{
				case DayOfWeek.Monday:
					shedule_polygon_image_mon.IsVisible = true;
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
					shedule_polygon_image_tue.IsVisible = true;
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
					shedule_polygon_image_wed.IsVisible = true;
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
					shedule_polygon_image_thu.IsVisible = true;
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
					shedule_polygon_image_fri.IsVisible = true;
					{
						mon_date.Text = date_Picker.Date.AddDays(-4).Date.Day.ToString();
						tue_date.Text = date_Picker.Date.AddDays(-3).Date.Day.ToString();
						wed_date.Text = date_Picker.Date.AddDays(-2).Date.Day.ToString();
						thu_date.Text = date_Picker.Date.AddDays(-1).Date.Day.ToString();
						fri_date.Text = date_Picker.Date.Day.ToString();
						sat_date.Text = date_Picker.Date.AddDays(1).Date.Day.ToString();
						sun_date.Text = date_Picker.Date.AddDays(2).Date.Day.ToString();
					}
					break;

				case DayOfWeek.Saturday:
					shedule_polygon_image_sat.IsVisible = true;
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
					shedule_polygon_image_sun.IsVisible = true;
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
			calculateNowDayOfWheek();
		}
		private void clearShedulePolygons()
		{
			shedule_polygon_image_mon.IsVisible = false;
			shedule_polygon_image_tue.IsVisible = false;
			shedule_polygon_image_wed.IsVisible = false;
			shedule_polygon_image_thu.IsVisible = false;
			shedule_polygon_image_fri.IsVisible = false;
			shedule_polygon_image_sat.IsVisible = false;
			shedule_polygon_image_sun.IsVisible = false;
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
						var dayDate = dateTimeToString(date_Picker.Date).Replace('.', '-'); //yyyy-mm-dd
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
					var dayDate = dateTimeToString(date_Picker.Date).Replace('.', '-'); //yyyy-mm-dd
					if (day.date == dayDate)
					{
						myColl.ItemsSource = day.lessons;
						break;
					}
				}
			}
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			configurePolygons();

			var currentWeekDate = dateTimeToString(date_Picker.Date); ///yyyy.mm.dd
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