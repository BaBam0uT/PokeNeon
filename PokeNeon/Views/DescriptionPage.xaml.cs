using PokeNeon.Models;
using PokeNeon.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescriptionPage : ContentPage
    {

        public Boolean fromSearchPage_;

        public int IdPokeASuppr;

        MyPokemon pokemonASuppr;

        public DescriptionPage(MyPokemon pokemon, Boolean fromSearchPage)
        {
            InitializeComponent();
            pokemonASuppr = pokemon;
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

        public async void SupprimerPoke(object sender, EventArgs e)
        {
            IdPokeASuppr = pokemonASuppr.ID;
            ListViewModel.Instance.ListePokemon.Remove(pokemonASuppr);
            await App.Database._database.DeleteAsync<MyPokemon>(IdPokeASuppr);
            ListViewModel.Instance.nbPokeAjoute--;
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