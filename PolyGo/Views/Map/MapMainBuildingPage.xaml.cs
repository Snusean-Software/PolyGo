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
					floor_Image.Source = "mb_Floor_2_Image.svg";
					floor = Floor.Second;
					floor_Number.Text = Convert.ToInt32(Floor.Second).ToString();
					break;

				case Floor.Second:
					floor_Image.Source = "mb_Floor_3_Image.svg";
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
					floor_Image.Source = "mb_Floor_1_Image.svg";
					floor = Floor.First;
					floor_Number.Text = Convert.ToInt32(Floor.First).ToString();
					break;

				case Floor.Third:
					floor_Image.Source = "mb_Floor_2_Image.svg";
					floor = Floor.Second;
					floor_Number.Text = Convert.ToInt32(Floor.Second).ToString();
					break;
			}
		}
	}
}

