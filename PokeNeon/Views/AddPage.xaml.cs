using Plugin.Media;
using Plugin.Media.Abstractions;
using PokeNeon.Models;
using PokeNeon.ViewModels;
using System;

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

        // Méthode asynchrone qui permet d'ajouter un Pokémon dans la PokemonList et dans la base de données en instanciant un nouveau Pokemon de type MyPokemon, à l'aide des valeurs des différents champs.
        // Si tous les champs ont bien été remplis sauf le 2ème type, le 2ème ability ou le 3ème ability qui sont optionnels, le Pokemon se créé et s'ajoute à la base de données, ainsi qu'à la PokemonList.
        // De plus, cela affiche un message de confirmation de création et ça réinitialise les valeurs de tous les champs. 
        // Sinon, l'application affiche un message d'erreur.
        // Entrée : un object sender, ainsi qu'une variable e de type EventArgs
        public async void AddPokemon(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(idEntry.Text) && !string.IsNullOrWhiteSpace(selectedImagePath)
                && !string.IsNullOrEmpty(type1Picker.SelectedItem.ToString()) && !string.IsNullOrWhiteSpace(heightEntry.Text) && !string.IsNullOrWhiteSpace(weightEntry.Text) 
                && !string.IsNullOrWhiteSpace(ability1Entry.Text) && !string.IsNullOrWhiteSpace(hpEntry.Text) && !string.IsNullOrWhiteSpace(attEntry.Text) && !string.IsNullOrWhiteSpace(defEntry.Text) 
                && !string.IsNullOrWhiteSpace(speattEntry.Text) && !string.IsNullOrWhiteSpace(spedefEntry.Text) && !string.IsNullOrWhiteSpace(speEntry.Text))
            {
                MyPokemon nouvPoke = new MyPokemon
                {
                    Name = nameEntry.Text.ToLower(),
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
                    Hp = hpEntry.Text,
                    Attack = attEntry.Text,
                    Defense = defEntry.Text,
                    SpeAttack = speattEntry.Text,
                    SpeDefense = spedefEntry.Text,
                    Speed = speEntry.Text,
                    isNew = true
                };
                ListViewModel.Instance.PokemonList.Insert(0, nouvPoke);
                ListViewModel.Instance.nbPokemonAdded++;
                await App.Database._database.InsertAsync(nouvPoke);

                nameEntry.Text = nameEntry.Text = string.Empty;
                idEntry.Text = idEntry.Text = string.Empty;
                selectedImage.Source = "add.png";
                selectedImagePath = null;
                type1Picker.SelectedItem = type1Picker.SelectedItem = string.Empty;
                type2Picker.SelectedItem = type2Picker.SelectedItem = string.Empty;
                heightEntry.Text = heightEntry.Text = string.Empty;
                weightEntry.Text = weightEntry.Text = string.Empty;
                ability1Entry.Text = ability1Entry.Text = string.Empty;
                ability2Entry.Text = ability2Entry.Text = string.Empty;
                ability3Entry.Text = ability3Entry.Text = string.Empty;
                hpEntry.Text = hpEntry.Text = string.Empty;
                attEntry.Text = attEntry.Text = string.Empty;
                defEntry.Text = defEntry.Text = string.Empty;
                speattEntry.Text = speattEntry.Text = string.Empty;
                spedefEntry.Text = spedefEntry.Text = string.Empty;
                speEntry.Text = speEntry.Text = string.Empty;
                addLabel.IsVisible = true;
            }
            else
            {
                await DisplayAlert("Error", "Missing information to enter!", "Ok");
                addLabel.IsVisible = false;
            }
        }

        // Méthode asynchrone qui permet de choisir dans la Galerie du téléphone une photo et de l'afficher sur la AddPage.
        // Si la photo ne peut pas être supportée par l'application, un message d'erreur apparaît sur l'écran.
        // Si l'utilisateur quitte la Galerie sans choisir d'image, un message d'erreur apparaît sur l'écran.
        // Entrée : un object sender, ainsi qu'une variable e de type EventArgs
        public async void ChooseImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Unsupported", "This feature is not supported", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await DisplayAlert("Error", "No image selected", "Ok");
                return;
            }

            selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
            selectedImagePath = selectedImageFile.Path;
        }
    }
}