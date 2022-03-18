using PokeNeon.Models;
using PokeNeon.ViewModels;
using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {

        public Boolean fromSearchPage;

        public SearchPage()
        {
            InitializeComponent();
            BindingContext = ListViewModel.Instance;
        }

        // Méthode asynchrone qui permet d'afficher la page de Description du Pokémon sur lequel on appuie et mets le booléen fromSearchPage à true pour le retour de la DescriptionPage à la SearchPage
        // Entrée : un object sender, ainsi qu'une variable e de type SelectionChangedEventArgs
        public async void DisplayDescription(object sender, SelectionChangedEventArgs e)
        {
            fromSearchPage = true;
            MyPokemon current = (e.CurrentSelection.FirstOrDefault() as MyPokemon);
            await Navigation.PushAsync(new DescriptionPage(current, fromSearchPage));
        }

        // Méthode qui permet de filtrer la listView sur le nom des Pokemon lorsque l'utilisateur saisit une lettre sur son clavier
        // Entrée : un object sender, ainsi qu'une variable e de type TextChangedEventArgs
        public void DisplayPokemonByEnteredName(object sender, TextChangedEventArgs e)
        {
            listview.ItemsSource = ListViewModel.Instance.PokemonList.Where(s => s.Name.StartsWith(e.NewTextValue));
        }
    }
}