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

        private async void AfficherDescription(object sender, SelectionChangedEventArgs e)
        {
            fromSearchPage = false;
            MyPokemon current = (e.CurrentSelection.FirstOrDefault() as MyPokemon);
            await Navigation.PushAsync(new DescriptionPage(current, fromSearchPage));
        }
    }
}