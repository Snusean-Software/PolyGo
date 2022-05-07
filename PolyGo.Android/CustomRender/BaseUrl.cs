using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PolyGo.Models.Navigation.CustomRender;
using Xamarin.Forms;

[assembly: Dependency(typeof(PolyGo.Droid.CustomRender.BaseUrl))]
namespace PolyGo.Droid.CustomRender
{
    public class BaseUrl : IBaseUrl
    {
        public string Get() { return "file:///android_asset/"; }
    }
}