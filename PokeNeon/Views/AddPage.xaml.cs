using Plugin.Media;
using Plugin.Media.Abstractions;
using PokeNeon.Models;
using PokeNeon.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public string selectedImagePath = "";

        public AddPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(idEntry.Text) && !string.IsNullOrWhiteSpace(selectedImagePath)
                && !string.IsNullOrEmpty(type1Picker.SelectedItem.ToString()) && !string.IsNullOrWhiteSpace(heightEntry.Text) && !string.IsNullOrWhiteSpace(weightEntry.Text) 
                && !string.IsNullOrWhiteSpace(ability1Entry.Text) && !string.IsNullOrWhiteSpace(pvEntry.Text) && !string.IsNullOrWhiteSpace(attEntry.Text) && !string.IsNullOrWhiteSpace(defEntry.Text) 
                && !string.IsNullOrWhiteSpace(speattEntry.Text) && !string.IsNullOrWhiteSpace(spedefEntry.Text) && !string.IsNullOrWhiteSpace(speEntry.Text))
            {
                MyPokemon nouvPoke = new MyPokemon
                {
                    Nom = nameEntry.Text,
                    Image = selectedImagePath,
                    IdPoke = "N° " + idEntry.Text,
                    TypeColor = ListViewModel.Instance.getTypeColor(type1Picker.SelectedItem.ToString().ToLower()),
                    Type1 = type1Picker.SelectedItem + ".png",
                    Type2 = type2Picker.SelectedItem + ".png",
                    Height = "Height : " + heightEntry.Text,
                    Weight = "Weight : " + weightEntry.Text,
                    Ability1 = ability1Entry.Text,
                    Ability2 = ability2Entry.Text,
                    Ability3 = ability3Entry.Text,
                    Pv = pvEntry.Text,
                    Attaque = attEntry.Text,
                    Defense = defEntry.Text,
                    Attaquespe = speattEntry.Text,
                    Defensespe = spedefEntry.Text,
                    Vitesse = speEntry.Text,
                    isNew = true
                };
                ListViewModel.Instance.ListePokemon.Insert(0, nouvPoke);
                await App.Database._database.InsertAsync(nouvPoke);

                nameEntry.Text = nameEntry.Text = string.Empty;
                idEntry.Text = idEntry.Text = string.Empty;
                selectionImage.Source = "ajout.png";
                selectedImagePath = null;
                type1Picker.SelectedItem = type1Picker.SelectedItem = string.Empty;
                type2Picker.SelectedItem = type2Picker.SelectedItem = string.Empty;
                heightEntry.Text = heightEntry.Text = string.Empty;
                weightEntry.Text = weightEntry.Text = string.Empty;
                ability1Entry.Text = ability1Entry.Text = string.Empty;
                ability2Entry.Text = ability2Entry.Text = string.Empty;
                ability3Entry.Text = ability3Entry.Text = string.Empty;
                pvEntry.Text = pvEntry.Text = string.Empty;
                attEntry.Text = attEntry.Text = string.Empty;
                defEntry.Text = defEntry.Text = string.Empty;
                speattEntry.Text = speattEntry.Text = string.Empty;
                spedefEntry.Text = spedefEntry.Text = string.Empty;
                speEntry.Text = speEntry.Text = string.Empty;
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
            selectedImagePath = selectedImageFile.Path;
        }
    }
}