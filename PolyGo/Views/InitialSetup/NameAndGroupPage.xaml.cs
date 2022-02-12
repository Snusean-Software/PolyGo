using System;

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
    void OnDoLaterButtonClicked(object sender, EventArgs e)
    {
      InSetupSupportFuncs.GoToMainShell(App.user);
    }
  }
}