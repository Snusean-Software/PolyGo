using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PolyGo.Shells
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class InitalSetupShell : Shell
  {
    public InitalSetupShell()
    {
      InitializeComponent();
    }
  }
}