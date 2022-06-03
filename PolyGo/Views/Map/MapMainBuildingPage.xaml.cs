using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.Models.Navigation;
using System.Collections.Generic;

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
		string startID = "null";
		string endID = "null";

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
	//	private void startChange(object sender, EventArgs e)
	//	//{
	//	//	if (start.Text.Length > 0) startFrame.IsVisible = true;
	//	//	List<FacultyGroup> places = new List<FacultyGroup>();
	//	//	int counter = 0;
	//	//	foreach (var gr in Groups)
	//	//	{
	//	//		if (gr.Name.StartsWith(entry.Text))
	//	//		{
	//	//			grps.Add(gr);
	//	//			++counter;
	//	//			if (counter > 10) break;
	//	//		}
	//	//	}
	//	//	groups.ItemsSource = grps;
	//	//	if (grps.Count == 0) startFrame.IsVisible = false;
	//	}
		
	//private void finishChange(object sender, EventArgs e)
	//{
	//	//{
	//	//	if (entry.Text.Length > 0) groupsFrame.IsVisible = true;
	//	//	List<FacultyGroup> grps = new List<FacultyGroup>();
	//	//	int counter = 0;
	//	//	foreach (var gr in Groups)
	//	//	{
	//	//		if (gr.Name.StartsWith(entry.Text))
	//	//		{
	//	//			grps.Add(gr);
	//	//			++counter;
	//	//			if (counter > 10) break;
	//	//		}
	//	//	}
	//	//	groups.ItemsSource = grps;
	//	//	if (grps.Count == 0) groupsFrame.IsVisible = false;
	//	endID = finish.Text.ToString();
	//}

	private void startChange(object sender, EventArgs e)
	{
		startID = start.Text.ToString();
	}
	private void finishChange(object sender, EventArgs e)
	{
		endID = finish.Text.ToString();
	}
	private void routChosen(object sender, EventArgs e)
		{
			map.clearMap();
			if (!map.drawPath(startID, endID))
			{
				// TODO error нет такого (таких) узлов
			}
			floor_Image.Source = ImageSource.FromStream(() => map.getMapStream(floor));
		}
	}
}

