using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PolyGo.SupportFuncs;
using PolyGo.Models;

namespace PolyGo.Views.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainProfilePage : ContentPage
	{
		private User user;
		private struct AccFileData
		{
			public bool Name { get; set; }
			public bool IsStudent { get; set; }
			public bool GroupNum { get; set; }
		}

		AccFileData AFD;
		public MainProfilePage()
		{
	
			InitializeComponent();

			content_page.BackgroundImageSource = ImageSource.FromResource("PolyGo.Resources.SetUp.first_background_image.png", GetType().Assembly);
			user = MainAppSupportFuncs.ParseAccFile();
			AFD.Name = !(user.Name is null);
			AFD.IsStudent = !(user.IsStudent is null);
			AFD.GroupNum = !(user.GroupNum is null);
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			BindingContext = user;
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
			MainAppSupportFuncs.ChangeAccParam(EditorName.Text, 
				MainAppSupportFuncs.AccParams.Name, AFD.Name);

			Button button = (Button)sender;
			button.IsVisible = false;
			ButtonEditName.IsVisible = true;
			AFD.Name = true;
			OnAppearing();
		}

		void OnSaveIsStudentClicked(object sender, EventArgs e)
		{
			EditorIsStudent.IsReadOnly = true;
			MainAppSupportFuncs.ChangeAccParam(EditorIsStudent.Text, 
				MainAppSupportFuncs.AccParams.IsStudent, AFD.IsStudent);

			Button button = (Button)sender;
			button.IsVisible = false;
			ButtonEditIsStudent.IsVisible = true;
			AFD.IsStudent = true;
			OnAppearing();
		}

		void OnSaveGroupNumClicked(object sender, EventArgs e)
		{
			EditorGroupNum.IsReadOnly = true;
			MainAppSupportFuncs.ChangeAccParam(EditorGroupNum.Text, 
				MainAppSupportFuncs.AccParams.GroupNum, AFD.GroupNum);

			Button button = (Button)sender;
			button.IsVisible = false;
			ButtonEditGroupNum.IsVisible = true;
			AFD.GroupNum = true;
			OnAppearing();
		}
	}
}