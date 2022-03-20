using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PolyGo.Views.Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainSchedulePage : TabbedPage
    {
        public MainSchedulePage()
        {
            InitializeComponent();
        }
    }
}