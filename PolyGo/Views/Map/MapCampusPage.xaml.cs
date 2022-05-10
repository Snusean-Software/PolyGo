using System;
using System.Reflection;
using System.IO;
using Xamarin.Forms;

using PolyGo.Models.Navigation.CustomRender;

namespace PolyGo.Views.Maps
{
  public partial class MapCampusPage : ContentPage
  {
    public MapCampusPage()
    {
      InitializeComponent();
      LoadMap();
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      LoadMap();
    }

    public void LoadMap()
    {
      var source = new HtmlWebViewSource();
      source.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
      var assembly = typeof(MapCampusPage).GetTypeInfo().Assembly;
      var stream = assembly.GetManifestResourceStream("PolyGo.Views.Map.index.html");
      StreamReader reader = null;
      if (stream != null)
      {
        try
        {
          reader = new StreamReader(stream);
          source.Html = reader.ReadToEnd();
        }
        finally
        {
          if (reader != null)
          {
            reader.Dispose();
          }
        }
        webView.Source = source;
      }
      else
      {
        DisplayAlert("Error!", "error on loading map occurred...", "Error!");
      }
    }

    async void webviewReloading(object sender, WebNavigatingEventArgs e)
    {
      Console.WriteLine("Nice");
      string result = await webView.EvaluateJavaScriptAsync($"getMapIDtoBeOpen()");
      switch (result)
      {
        case "1": // MainBuilding
          webView.Reload();
          await Shell.Current.GoToAsync($"{nameof(MapMainBuildingPage)}?");
          break;
        default:
          break;
      }
    }
  }
}