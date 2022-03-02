using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using PokeNeon.ViewModels;
using System.Diagnostics;
using System;

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

        private async void AfficherDescription(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DescriptionPage());
        }
    }
}