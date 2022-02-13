using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PolyGo.Views.Map;

namespace PolyGo.Shells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(MapMainBuildingPage), typeof(MapMainBuildingPage));
			Routing.RegisterRoute(nameof(Map11corpusPage), typeof(Map11corpusPage));
		}
	}
}