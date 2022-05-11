using System;
using System.Reflection;
using System.IO;
using Xamarin.Forms;

using PolyGo.Models.Navigation.CustomRender;
using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;
using Newtonsoft.Json;

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

    async void openBuildingMap(object sender, WebNavigatingEventArgs e)
    {
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

    async void showMarkers(object sender, EventArgs e)
    {
      await webView.EvaluateJavaScriptAsync(@"showMarkers()");
    }

    async void hideMarkers(object sender, EventArgs e)
    {
      await webView.EvaluateJavaScriptAsync(@"hideMarkers()");
    }

    async void showRoute(object sender, EventArgs e)
    {
      var routerDb = new RouterDb();
      var assembly = typeof(MapCampusPage).GetTypeInfo().Assembly;

      using (var stream = assembly.GetManifestResourceStream("PolyGo.Resources.map.campus.campus.osm.pbf"))
      {
        routerDb.LoadOsmData(stream, Vehicle.Pedestrian);
      }
      routerDb.AddContracted(routerDb.GetSupportedProfile("Pedestrian"));
      var router = new Router(routerDb);

      //calculate a route.
      var route = router.Calculate(Vehicle.Pedestrian.Fastest(),
      59.998979957245f, 30.3656656413452f, 60.0069869464369f, 30.3840933978821f);
      var geoJson = route.ToGeoJson();

      Console.WriteLine(geoJson);
      var s = JsonConvert.DeserializeObject(geoJson);

      await webView.EvaluateJavaScriptAsync($"addGeoJson({s})");
    }
  }
}