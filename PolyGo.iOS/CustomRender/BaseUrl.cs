using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PolyGo.Models.Navigation.CustomRender;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PolyGo.iOS.CustomRender.BaseUrl))]
namespace PolyGo.iOS.CustomRender
{
    public class BaseUrl : IBaseUrl
    {
        public string Get() { return NSBundle.MainBundle.BundlePath; }
    }
}