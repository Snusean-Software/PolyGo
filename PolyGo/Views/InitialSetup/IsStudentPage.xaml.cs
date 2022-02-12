using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.SupportFuncs;
using PolyGo.Resx;

namespace PolyGo.Views.InitialSetup
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class IsStudentPage : ContentPage
  {
    public IsStudentPage()
    {
      InitializeComponent();
    }
    async void OnNextButtonClicked(object sender, EventArgs e)
    {
      if (rbStudent.IsChecked)
      {
        App.user.IsStudent = true;
        await Shell.Current.GoToAsync($"{nameof(NameAndGroupPage)}?");
      }
      else if (rbTeacher.IsChecked)
      {
        App.user.IsStudent = false;
        await Shell.Current.GoToAsync($"{nameof(TeacherListPage)}?");
      }
      else
      {
        await DisplayAlert(AppResources.Notification, AppResources.NotifTextIsStudent, AppResources.NotifYes);
      }
    }
    void OnDoLaterButtonClicked(object sender, EventArgs e)
    {
      App.user.IsStudent = null;
      InSetupSupportFuncs.GoToMainShell(App.user);
    }
  }
}