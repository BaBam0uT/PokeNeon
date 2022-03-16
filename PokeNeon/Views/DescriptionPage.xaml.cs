using PokeNeon.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescriptionPage : ContentPage
    {

        public Boolean fromSearchPage_;

        public DescriptionPage(MyPokemon pokemon, Boolean fromSearchPage)
        {
            InitializeComponent();
            BindingContext = pokemon;
            fromSearchPage_ = fromSearchPage;
        }

        private async void RevenirListe(object sender, EventArgs e)
        {
            if (fromSearchPage_ == true)
            {
                await Navigation.PushAsync(new SearchPage());
            }
            else
            {
                await Navigation.PushAsync(new ListPage());
            }
        }
    }
}