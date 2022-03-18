using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PokeNeon.ViewModels;
using System;
using PokeNeon.Models;
using System.Linq;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {

        public Boolean fromSearchPage;

        public ListPage()
        {
            InitializeComponent();
            BindingContext = ListViewModel.Instance;
        }

        // Méthode asynchrone qui permet d'afficher la page de Description du Pokémon sur lequel on appuie et mets le booléen fromSearchPage à false pour le retour de la DescriptionPage à la ListPage
        // Entrée : un object sender, ainsi qu'une variable e de type SelectionChangedEventArgs
        public async void DisplayDescription(object sender, SelectionChangedEventArgs e)
        {
            fromSearchPage = false;
            MyPokemon current = (e.CurrentSelection.FirstOrDefault() as MyPokemon);
            await Navigation.PushAsync(new DescriptionPage(current, fromSearchPage));
        }
    }
}