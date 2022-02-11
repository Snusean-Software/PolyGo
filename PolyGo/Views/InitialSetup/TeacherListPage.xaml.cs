using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.SupportFuncs;

namespace PolyGo.Views.InitialSetup
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class TeacherListPage : ContentPage
  {
    public TeacherListPage()
    {
      InitializeComponent();
    }
    async void OnNextButtonClicked(object sender, EventArgs e)
    {
    }
    async void OnDoLaterButtonClicked(object sender, EventArgs e)
    {
      InSetupSupportFuncs.GoToMainShell(App.user);
    }
  }
}