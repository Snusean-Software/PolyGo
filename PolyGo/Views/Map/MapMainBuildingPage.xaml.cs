using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.Models.Navigation;

namespace PolyGo.Views.Maps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapMainBuildingPage : ContentPage
	{
		Map map = new Map(MapConstants.MapID.mMainBuilding);
		public MapMainBuildingPage()
		{
			InitializeComponent();
			floor_Image.Source = ImageSource.FromStream(() => map.getMapStream(MapConstants.Floor.First));
			up_Button.Source = ImageSource.FromResource("PolyGo.Resources.map.up_button.png", GetType().Assembly);
			down_Button.Source = ImageSource.FromResource("PolyGo.Resources.map.down_button.png", GetType().Assembly);
			content_page.BackgroundColor = Color.Ivory;
		}

		MapConstants.Floor floor = MapConstants.Floor.First;

		private void onUpButtonClicked(object sender, EventArgs e)
		{
			switch (floor)
			{
				case MapConstants.Floor.First:
					floor = MapConstants.Floor.Second;
					floor_Image.Source = ImageSource.FromStream(() => map.getMapStream(floor));
					floor_Number.Text = (Convert.ToInt32(floor) + 1).ToString();
					break;

				case MapConstants.Floor.Second:
					floor = MapConstants.Floor.Third;
					floor_Image.Source = ImageSource.FromStream(() => map.getMapStream(floor));
					floor_Number.Text = (Convert.ToInt32(floor) + 1).ToString();
					break;

				case MapConstants.Floor.Third:
					break;
			}
		}
		private void onDownButtonClicked(object sender, EventArgs e)
		{
			switch (floor)
			{
				case MapConstants.Floor.First:
					break;

				case MapConstants.Floor.Second:
					floor = MapConstants.Floor.First;
					floor_Image.Source = ImageSource.FromStream(() => map.getMapStream(floor));
					floor_Number.Text = (Convert.ToInt32(floor) + 1).ToString();
					break;

				case MapConstants.Floor.Third:
					floor = MapConstants.Floor.Second;
					floor_Image.Source = ImageSource.FromStream(() => map.getMapStream(floor));
					floor_Number.Text = (Convert.ToInt32(floor) + 1).ToString();
					break;
			}
		}
		private void startChange(object sender, EventArgs e)
		{

		}
		private void finishChange(object sender, EventArgs e)
		{

		}
		private void routChosen(object sender, EventArgs e)
		{

		}
	}
}

