using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PolyGo.Views.InitialSetup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupNumberPage : ContentPage
	{
		public GroupNumberPage()
		{
			InitializeComponent();
			content_page.BackgroundImageSource = ImageSource.FromResource("PolyGo.Resources.SetUp.second_background_image.png", GetType().Assembly);

		}
	}
}