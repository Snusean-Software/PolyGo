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
        await DisplayAlert("Уведомление", "Пожалуйста, выберите ваше положение", "Да, конечно");
      }
    }
    void OnDoLaterButtonClicked(object sender, EventArgs e)
    {
      App.user.IsStudent = null;
      InSetupSupportFuncs.GoToMainShell(App.user);
    }
  }
}