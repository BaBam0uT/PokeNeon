using Plugin.Media;
using Plugin.Media.Abstractions;
using PokeNeon.Models;
using PokeNeon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeNeon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                MyPokemon nouvPoke = new MyPokemon
                {
                    Nom = nameEntry.Text,
                    Image = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/1.png",
                    IdPoke = "N° " + idEntry.Text,
                    Type1 = "grass.png",
                    Type2 = "poison.png",
                    TypeColor = "#78C850",
                    Height = "Height : " + "50",
                    Weight = "Weight : " + "50",
                    Ability1 = "Ability 1",
                    Ability2 = "Ability 2",
                    Ability3 = "Ability 3",
                    Pv = "50",
                    Attaque = "50",
                    Defense = "50",
                    Attaquespe = "50",
                    Defensespe = "50",
                    Vitesse = "50"
                };
                ListViewModel.Instance.ListePokemon.Add(nouvPoke);
                await App.Database.SaveMyPokemonAsync(nouvPoke);
                nameEntry.Text = nameEntry.Text = string.Empty;
                idEntry.Text = idEntry.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Erreur", "Il manque des informations à saisir !", "Ok");
            }
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Non supporté", "Cette fonctionnalité n'est pas supportée", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await DisplayAlert("Erreur", "Aucune image sélectionnée", "Ok");
                return;
            }

            selectionImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
        }
    }
}