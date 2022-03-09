using PokeNeon.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescriptionPage : ContentPage
    {
        public DescriptionPage(MyPokemon pokemon)
        {
            InitializeComponent();
            BindingContext = pokemon;
        }

        private async void RevenirListe(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListPage());
        }
    }
}