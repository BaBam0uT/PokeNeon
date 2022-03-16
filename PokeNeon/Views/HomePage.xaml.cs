using Android;
using Android.Media;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        MediaPlayer player;

        public HomePage()
        {
            InitializeComponent();
        }
    }
}