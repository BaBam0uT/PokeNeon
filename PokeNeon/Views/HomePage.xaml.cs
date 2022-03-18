using PokeNeon.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public int sliderNumber;

        public HomePage()
        {
            InitializeComponent();
        }

        // Méthode qui permet de modifier en direct la valeur du Slider
        // Entrée : un object sender, ainsi qu'une variable e de type ValueChangedEventArgs
        public void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            sliderNumber = (int)nbSlider.Value;
            sliderNbLabel.Text = String.Format("{0}", sliderNumber);
        }

        // Méthode asynchrone qui permet de modifier le nombre de Pokemon récupéré depuis l'API par la valeur du Slider
        // Entrée : un object sender, ainsi qu'une variable e de type EventArgs
        public async void ChangeList(object sender, EventArgs e)
        {
            sliderNumber = (int)nbSlider.Value;
            await ListViewModel.Instance.ChangeApiList(sliderNumber);
        }
    }
}