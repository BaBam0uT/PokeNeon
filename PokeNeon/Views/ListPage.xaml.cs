using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PokeNeon.ViewModels;
using System;
using PokeNeon.Models;
using System.Linq;
using System.Windows.Input;
using System.Diagnostics;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {

        public ListPage()
        {
            InitializeComponent();
            BindingContext = ListViewModel.Instance;
        }

        private async void AfficherDescription(object sender, SelectionChangedEventArgs e)
        {
            MyPokemon current = (e.CurrentSelection.FirstOrDefault() as MyPokemon);
            await Navigation.PushAsync(new DescriptionPage(current));
        }
    }
}