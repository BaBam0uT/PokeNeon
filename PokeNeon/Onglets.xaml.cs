using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Onglets : TabbedPage
    {
        public Onglets()
        {
            InitializeComponent();
        }
    }
}