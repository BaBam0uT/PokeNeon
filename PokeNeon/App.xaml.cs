using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Onglets();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
