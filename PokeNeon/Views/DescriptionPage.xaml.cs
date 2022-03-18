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

        public int IdPokemonToRemove;

        public MyPokemon pokemonToRemove;

        public DescriptionPage(MyPokemon pokemon, Boolean fromSearchPage)
        {
            InitializeComponent();
            BindingContext = pokemon;
            pokemonToRemove = pokemon;
            fromSearchPage_ = fromSearchPage;
        }

        // Méthode asynchrone qui permet de revenir soit dans la SearchPage, soit dans la ListPage, en fonction de la valeur du booléen fromSearchPage_
        // Entrée : un object sender, ainsi qu'une variable e de type EventArgs
        private async void ReturnTo(object sender, EventArgs e)
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

        // Méthode asynchrone qui permet de supprimer de la PokemonList et de la base de données un Pokemon qui a été ajouté par l'utilisateur et ensuite de revenir soit dans la SearchPage,
        // soit dans la ListPage, en fonction de la valeur du booléen fromSearchPage_
        // Entrée : un object sender, ainsi qu'une variable e de type EventArgs
        public async void RemovePokemon(object sender, EventArgs e)
        {
            IdPokemonToRemove = pokemonToRemove.ID;
            ListViewModel.Instance.PokemonList.Remove(pokemonToRemove);
            await App.Database._database.DeleteAsync<MyPokemon>(IdPokemonToRemove);
            ListViewModel.Instance.nbPokemonAdded--;
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