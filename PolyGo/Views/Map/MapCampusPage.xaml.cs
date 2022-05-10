using System;
using System.Reflection;
using System.IO;
using Xamarin.Forms;

using PolyGo.Models.Navigation.CustomRender;

namespace PolyGo.Views.Map
{
  public partial class MapCampusPage : ContentPage
  {
    public MapCampusPage()
    {
      InitializeComponent();
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

    public void SetItemMuestra()
    {
      //-2.14003,-79.9312967 marker
      newMarker("-2.14003", "-79.9312967", "Soy un marker");

      //-2.1407283,-79.9286524 circulo
      newCircle("-2.1407283", "-79.9286524", "red", "#07", 0.5, 50);

      //-2.14003,-79.9312967 / -2.1407283,-79.9286524 Linea
      newLine("-2.14003", "-79.9312967", "-2.1407283", "-79.9286524", "blue");

      //mostrar objetos
      Show();

      //centrar el mapa
      //CentrarMapa("10.6222200", "-66.5735300");
    }

    /*
     Crear marcador
     lat = latitud
     lon = longitud
     */
    public void newMarker(string lat, string lon, string texto = "")
    {
      if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lon))
      {
        Console.Write("Verifique los campos lat lon");
        return;
      }

      webView.Eval(string.Format("newMarker({0}, {1}, {2})", "\"" + lat + "\"", "\"" + lon + "\"", "\"" + texto + "\""));
    }

    /*
     Crear Linea
     latFrom = latitud de comienzo
     lonFrom = longitud de comienzo
     latTo = latitud final
     lonTo = longitud final
     color = color de la linea
     */
    public void newLine(string latFrom, string lonFrom, string latTo, string LonTo, string Color = "blue")
    {
      if (string.IsNullOrEmpty(latFrom) || string.IsNullOrEmpty(lonFrom) || string.IsNullOrEmpty(latTo) || string.IsNullOrEmpty(LonTo))
      {
        Console.Write("Verifique los campos lat lon");
        return;
      }
      webView.Eval(string.Format("newLine({0},{1},{2}),{3},{4}", "\"" + latFrom + "\"", "\"" + lonFrom + "\"", "\"" + latTo + "\"", "\"" + LonTo + "\"", "\"" + Color + "\""));
    }

    /*
     Crear Circulo
     lat = latutud
     lon = longitud
     color = color de linea
     fillcolor = color de relleno
     fillopacity = transparencia de relleno
     raidus = radio
     */
    public void newCircle(string lat, string lon, string color = "blue", string fillcolor = "#07", double fillopacity = 0.5, int radius = 500)
    {
      if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lon))
      {
        Console.Write("Verifique los campos lat lon");
        return;
      }
      webView.Eval(string.Format("newCircle({0},{1},{2},{3},{4},{5})", "\"" + lat + "\"", "\"" + lon + "\"", "\"" + color + "\"", "\"" + fillcolor + "\"", "\"" + fillopacity + "\"", "\"" + radius + "\""));
    }

    /*
     Centrar mapa
     lat = latitud
     lon = longitud
     */
    public void CentrarMapa(string lat, string lon)
    {
      if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lon))
      {
        Console.Write("Verifique los campos lat lon");
        return;
      }
      webView.Eval(string.Format("centerMap({0},{1})", "\"" + lat + "\"", "\"" + lon + "\""));
    }

    /*
     Mostrar grupo
     */
    public void Show()
    {
      webView.Eval(@"show()");
    }

    /*
     Eliminar objetos
     */
    public void Destruir()
    {
      webView.Eval(@"delMarket()");
    }

    private void Send_Clicked(object sender, EventArgs e)
    {
      SetItemMuestra();
    }
  }
}