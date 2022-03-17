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

        private async void AfficherDescription(object sender, SelectionChangedEventArgs e)
        {
            fromSearchPage = true;
            MyPokemon current = (e.CurrentSelection.FirstOrDefault() as MyPokemon);
            await Navigation.PushAsync(new DescriptionPage(current, fromSearchPage));
        }

        public void AffichageDesPokemon(object sender, TextChangedEventArgs e)
        {
            listview.ItemsSource = ListViewModel.Instance.ListePokemon.Where(s => s.Nom.StartsWith(e.NewTextValue));
        }
    }
}