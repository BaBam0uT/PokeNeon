using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
    }
}