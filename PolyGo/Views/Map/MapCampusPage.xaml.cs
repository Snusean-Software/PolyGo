using System;
using System.Reflection;
using System.IO;
using Xamarin.Forms;

using PolyGo.Models.Navigation;
using PolyGo.Models.Navigation.CustomRender;
using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;
using Newtonsoft.Json;
using System.Collections.Generic;
using PolyGo.Models.Navigation.Campus;

namespace PolyGo.Views.Maps
{
  public partial class MapCampusPage : ContentPage
  {
    List<Place> Places = App.RtsDatabase.Places;
    List<Models.Navigation.Campus.Route> Routes = App.RtsDatabase.Routes;
    string StartID
    {
      get
      {
        return start.Text;
      }
    }
    string EndID
    {
      get
      {
        return finish.Text;
      }
    }
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

    private void startChange(object sender, EventArgs e)
    {
      if (start.Text.Length > 0) startFrame.IsVisible = true;
      List<Place> pls = new List<Place>();
      int counter = 0;
      foreach (var pl in Places)
      {
        if (pl.Name.ToLower().StartsWith(start.Text))
        {
          pls.Add(pl);
          ++counter;
          if (counter > 10) break;
        }
      }
      startPoints.ItemsSource = pls;
      if (pls.Count == 0) startFrame.IsVisible = false;
    }

    private void finishChange(object sender, EventArgs e)
    {
      if (finish.Text.Length > 0) finishFrame.IsVisible = true;
      List<Place> pls = new List<Place>();
      int counter = 0;
      foreach (var pl in Places)
      {
        if (pl.Name.ToLower().StartsWith(finish.Text))
        {
          pls.Add(pl);
          ++counter;
          if (counter > 10) break;
        }
      }
      finishPoints.ItemsSource = pls;
      if (pls.Count == 0) finishFrame.IsVisible = false;
    }
    private async void routChosen(object sender, EventArgs e)
    {
      var temp = JsonConvert.DeserializeObject(Routes.Find((x => x.Start.StartsWith(StartID) && x.End.StartsWith(EndID))).GeoJSON); //КОСТЫЛЬ, ТАК НЕПРАВИЛЬНО

      await webView.EvaluateJavaScriptAsync($"addGeoJson({temp})");
    }
    private void onStartTapped(object sender, EventArgs e)
    {
      var label = sender as Label;
      start.Text = label.Text;
      startFrame.IsVisible = false;
    }
    private void onFinishTapped(object sender, EventArgs e)
    {
      var label = sender as Label;
      finish.Text = label.Text;
      finishFrame.IsVisible = false;
    }
  }
}