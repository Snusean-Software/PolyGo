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
  public partial class NameAndGroupPage : ContentPage
  {
    public NameAndGroupPage()
    {
      InitializeComponent();
    }
    void OnFinishButtonClicked(object sender, EventArgs e)
    {
      InSetupSupportFuncs.GoToMainShell(App.user);
    }
  }
}