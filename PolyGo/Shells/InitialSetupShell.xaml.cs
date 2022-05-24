using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.Views.InitialSetup;

namespace PolyGo.Shells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InitialSetupShell : Shell
	{
		public InitialSetupShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(IsStudentPage), typeof(IsStudentPage));
			Routing.RegisterRoute(nameof(TeacherListPage), typeof(TeacherListPage));
			Routing.RegisterRoute(nameof(NameAndGroupPage), typeof(NameAndGroupPage));
			Routing.RegisterRoute(nameof(GroupNumberPage), typeof(GroupNumberPage));
			Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
		}
	}
}