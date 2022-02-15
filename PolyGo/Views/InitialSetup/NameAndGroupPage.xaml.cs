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
      if (!string.IsNullOrWhiteSpace(ed1.Text))
      {
        App.user.Name = ed1.Text;
        if(!string.IsNullOrWhiteSpace(ed2.Text))
				{
          App.user.GroupNum = ed2.Text;
          InSetupSupportFuncs.GoToMainShell(App.user);
        }
				else
				{
          ed2.Placeholder = "Please write your name";
          ed2.PlaceholderColor = Color.Red;
        }
      }
      else
      {
        ed1.Placeholder = "Please write your name";
        ed1.PlaceholderColor = Color.Red;
      }
    }
    void OnDoLaterButtonClicked(object sender, EventArgs e)
    {
      App.user.GroupNum = null;
      InSetupSupportFuncs.GoToMainShell(App.user);
    }
  }
}