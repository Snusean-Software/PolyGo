using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PolyGo.Shells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
		}
	}
}