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

		List<Node> Places = new List<Node>();

		public MapMainBuildingPage()
		{
			InitializeComponent();
			floor_Image.Source = ImageSource.FromStream(() => map.getMapStream(MapConstants.Floor.First));
			up_Button.Source = ImageSource.FromResource("PolyGo.Resources.map.up_button.png", GetType().Assembly);
			down_Button.Source = ImageSource.FromResource("PolyGo.Resources.map.down_button.png", GetType().Assembly);
			content_page.BackgroundColor = Color.Ivory;

			foreach(var nd in App.MpDatabase.getNodes((int)MapConstants.MapID.mMainBuilding))
			{
				if (nd.Classroom != "-") Places.Add(nd);
			}
		}

		MapConstants.Floor floor = MapConstants.Floor.First;
		string StartID
		{
			get
			{
				return start.Text;
			}
		}
		string EndID
		{
			get
			{
					return finish.Text;
			}
		}
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
			if (start.Text.Length > 0) startFrame.IsVisible = true;
			List<Node> pls = new List<Node>();
			int counter = 0;
			foreach (var pl in Places)
			{
				if (pl.Classroom.StartsWith(start.Text.ToLower()))
				{
					pls.Add(pl);
					++counter;
					if (counter > 10) break;
				}
			}
			startPoints.ItemsSource = pls;
			if (pls.Count == 0) startFrame.IsVisible = false;
		}

	private void finishChange(object sender, EventArgs e)
	{
			if (finish.Text.Length > 0) finishFrame.IsVisible = true;
			List<Node> pls = new List<Node>();
			int counter = 0;
			foreach (var pl in Places)
			{
				if (pl.Classroom.StartsWith(finish.Text.ToLower()))
				{
					pls.Add(pl);
					++counter;
					if (counter > 10) break;
				}
			}
			finishPoints.ItemsSource = pls;
			if (pls.Count == 0) finishFrame.IsVisible = false;
		}
	private void routChosen(object sender, EventArgs e)
		{
			map.clearMap();
			if (!map.drawPath(StartID, EndID))
			{
				// TODO error нет такого (таких) узлов
			}
			floor_Image.Source = ImageSource.FromStream(() => map.getMapStream(floor));
		}
		private void onStartTapped(object sender, EventArgs e)
		{
			var label = sender as Label;
			start.Text = label.Text;
			startFrame.IsVisible = false;
		}
		private void onFinishTapped(object sender, EventArgs e)
		{
			var label = sender as Label;
			finish.Text = label.Text;
			finishFrame.IsVisible = false;
		}
	}
}

