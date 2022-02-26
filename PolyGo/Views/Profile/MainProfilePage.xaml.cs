using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.SupportFuncs;

namespace PolyGo.Views.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainProfilePage : ContentPage
	{
		public MainProfilePage()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = MainAppSupportFuncs.ParseAccFile();
		}

	  void OnEditNameClicked(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			EditorName.IsReadOnly = false;
			button.IsVisible = false;
			ButtonSaveName.IsVisible = true;
		}

		void OnEditIsStudentClicked(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			EditorIsStudent.IsReadOnly = false;
			button.IsVisible = false;
			ButtonSaveIsStudent.IsVisible = true;
		}

		void OnEditGroupNumClicked(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			EditorGroupNum.IsReadOnly = false;
			button.IsVisible = false;
			ButtonSaveGroupNum.IsVisible = true;
		}

		void OnSaveNameClicked(object sender, EventArgs e)
		{
			EditorName.IsReadOnly = true;
			MainAppSupportFuncs.ChangeAccParam(EditorName.Text, MainAppSupportFuncs.AccParams.Name);

			Button button = (Button)sender;
			button.IsVisible = false;
			ButtonEditName.IsVisible = true;
			OnAppearing();
		}

		void OnSaveIsStudentClicked(object sender, EventArgs e)
		{
			EditorIsStudent.IsReadOnly = true;
			MainAppSupportFuncs.ChangeAccParam(EditorIsStudent.Text, MainAppSupportFuncs.AccParams.IsStudent);

			Button button = (Button)sender;
			button.IsVisible = false;
			ButtonEditIsStudent.IsVisible = true;
			OnAppearing();
		}

		void OnSaveGroupNumClicked(object sender, EventArgs e)
		{
			EditorGroupNum.IsReadOnly = true;
			MainAppSupportFuncs.ChangeAccParam(EditorGroupNum.Text, MainAppSupportFuncs.AccParams.GroupNum);

			Button button = (Button)sender;
			button.IsVisible = false;
			ButtonEditGroupNum.IsVisible = true;
			OnAppearing();
		}
	}
}