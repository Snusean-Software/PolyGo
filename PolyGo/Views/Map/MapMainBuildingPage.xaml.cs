using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PolyGo.Views.Map
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapMainBuildingPage : ContentPage
	{
		public MapMainBuildingPage()
		{
			InitializeComponent();
			floor_Image.Source = ImageSource.FromResource("PolyGo.Resources.map.mb.floor_1.png", GetType().Assembly);
			up_Button.Source = ImageSource.FromResource("PolyGo.Resources.map.up_button.png", GetType().Assembly); ;
			down_Button.Source = ImageSource.FromResource("PolyGo.Resources.map.down_button.png", GetType().Assembly); ;
			decoration.Source = ImageSource.FromResource("PolyGo.Resources.map.start_to_finish_image.png", GetType().Assembly); ;
		}

    public enum Floor: ushort
		{
			First = 1,
			Second = 2, 
			Third = 3,
    }

		Floor floor = Floor.First;

		private void onUpButtonClicked(object sender, EventArgs e)
		{
			switch(floor)
      {
				case Floor.First:
					floor_Image.Source = ImageSource.FromResource("PolyGo.Resources.map.mb.floor_2.png", GetType().Assembly);
					floor = Floor.Second;
					floor_Number.Text = Convert.ToInt32(Floor.Second).ToString();
					break;

				case Floor.Second:
					floor_Image.Source = ImageSource.FromResource("PolyGo.Resources.map.mb.floor_3.png", GetType().Assembly);
					floor = Floor.Third;
					floor_Number.Text = Convert.ToInt32(Floor.Third).ToString();
					break;

				case Floor.Third:
					break;
			}
		}
		private void onDownButtonClicked(object sender, EventArgs e)
		{
			switch (floor)
			{
				case Floor.First:
					break;

				case Floor.Second:
					floor_Image.Source = ImageSource.FromResource("PolyGo.Resources.map.mb.floor_1.png", GetType().Assembly);
					floor = Floor.First;
					floor_Number.Text = Convert.ToInt32(Floor.First).ToString();
					break;

				case Floor.Third:
					floor_Image.Source = ImageSource.FromResource("PolyGo.Resources.map.mb.floor_2.png", GetType().Assembly);
					floor = Floor.Second;
					floor_Number.Text = Convert.ToInt32(Floor.Second).ToString();
					break;
			}
		}
	}
}

