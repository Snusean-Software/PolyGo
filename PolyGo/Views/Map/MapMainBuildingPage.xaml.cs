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

		int floor = 1;
		
		private void upButton(object sender, EventArgs e)
		{
            if (floor < 3)
            {
				if(floor == 1)
                {
					floorImage.Source = "gz2floor.png";
				}
                else
                {
					floorImage.Source = "gz3floor.png";
				}
				floor++;
				floorNumber.Text = Convert.ToString(floor);

			}
		}
		private void downButton(object sender, EventArgs e)
		{
			if (floor > 1)
			{
				if (floor == 3)
				{
					floorImage.Source = "gz2floor.png";
				}
				else
				{
					floorImage.Source = "gz1floor.png";
				}
				floor--;
				floorNumber.Text = Convert.ToString(floor);
			}
		}

		

	}
}

