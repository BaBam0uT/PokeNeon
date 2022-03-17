using PokeNeon.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public int nouvNb;

        public HomePage()
        {
            InitializeComponent();
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            nouvNb = (int)nbSlider.Value;
            nbLabel.Text = String.Format("{0}", nouvNb);
        }

        public async void ChangerListe(object sender, EventArgs e)
        {
            nouvNb = (int)nbSlider.Value;
            Debug.WriteLine("nbPokeVoulu : " + nouvNb);
            await ListViewModel.Instance.ChangeList(nouvNb);
        }
    }
}