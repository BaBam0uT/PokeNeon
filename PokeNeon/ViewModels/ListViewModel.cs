using PokeApiNet;
using PokeNeon.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PokeNeon.ViewModels
{
    // Cette classe est le ViewModel de type BaseViewModel qui va permettre de faire le lien de connexion entre les Models et les Views.
    // On va convertir les objets de données du model MyPokemon de sorte que ces objets soient facilement gérés et présentés.
    // Ici, on va rassembler les méthodes qui vont permettre de récupérer les Pokemon de l'API ou/et les Pokemon stockés en base de données et de les ajouter à une liste de Pokemon de type ObservableCollection nommée PokemonList.
    public class ListViewModel : BaseViewModel
    {
        private static ListViewModel _instance = new ListViewModel();

        public static ListViewModel Instance { get { return _instance; } }

        public const int NUMBER_OF_POKEMON_API_AT_STARTUP = 50;

        public int nbPokemonAdded = 0;

        public int nbPokemonDatabase = 0;

        public MyPokemon pokemonFromDatabase;

        public ObservableCollection<MyPokemon> PokemonList
        {
            get { return GetValue<ObservableCollection<MyPokemon>>(); }
            set { SetValue(value); }
        }

        public ListViewModel() {
            GetList();
        }

        // Méthode asynchrone qui permet de récupérer au démarrage de l'application 50 Pokemon de l'API s'il s'agit de la première ouverture de l'application et les stocker en base de données et les ajouter à la PokemonList.
        // Si ce n'est pas la première ouverture de l'application, l'application va d'abord récupérer les Pokemon qui ont été ajoutés par l'utilisateur et les ajouter à la PokemonList,
        // ensuite l'application va récupérer les 50 Pokemon stockés en base de données, et les ajouter à la PokemonList.
        // Entrée : Rien
        public async void GetList() {
            PokeApiClient pokeClient = new PokeApiClient();
            PokemonList = new ObservableCollection<MyPokemon>();
            nbPokemonDatabase = await App.Database._database.Table<MyPokemon>().CountAsync();
            if (PokemonList.Count == 0)
            {
                for (var i = 0; i < nbPokemonDatabase; i++)
                {
                    pokemonFromDatabase = await App.Database._database.Table<MyPokemon>().ElementAtAsync(i);
                    if (pokemonFromDatabase.isNew == true)
                    {
                        PokemonList.Add(pokemonFromDatabase);
                        nbPokemonAdded++;
                    }
                }
                if (nbPokemonDatabase < NUMBER_OF_POKEMON_API_AT_STARTUP)
                {
                    for (var j = 1; j <= NUMBER_OF_POKEMON_API_AT_STARTUP; j++)
                    {
                        Pokemon p = await Task.Run(() =>
                        pokeClient.GetResourceAsync<Pokemon>(j));
                        sendPokemonApiToList(p);
                    }
                }
                else
                {
                    for (var j = 0; j < NUMBER_OF_POKEMON_API_AT_STARTUP; j++)
                    {
                        pokemonFromDatabase = await App.Database._database.Table<MyPokemon>().ElementAtAsync(j);
                        PokemonList.Add(pokemonFromDatabase);
                    }
                }
            }
        }

        // Méthode asynchrone qui permet de modifier le nombre de Pokemon de la PokemonList venant de l'API.
        // Entrée : un entier qui correspond au nouveau nombre de Pokemon de l'API qu'on veut récupérer.
        public async Task ChangeApiList(int newNbOfPokemonAPIList)
        {
            PokeApiClient pokeClient = new PokeApiClient();
            // Permet de supprimer de la PokemonList des Pokemon de l'API si on souhaite un nombre de Pokemon venant de l'API moins élevé
            if (newNbOfPokemonAPIList < PokemonList.Count)
            {
                for (var i = PokemonList.Count; i > newNbOfPokemonAPIList + nbPokemonAdded; i--)
                {
                    PokemonList.RemoveAt(i - 1);
                }
            }
            // Permet d'ajouter dans la PokemonList des Pokemon de l'API si on souhaite un nombre de Pokemon venant de l'API plus élevé
            else if (newNbOfPokemonAPIList > PokemonList.Count)
            {
                // Si le nombre de Pokemon de l'API actuel dans la liste est inférieur à 50
                if (PokemonList.Count - nbPokemonAdded < NUMBER_OF_POKEMON_API_AT_STARTUP)
                {
                    // Si le nombre de Pokemon de l'API dans la liste qu'on souhaite est supérieur à 50
                    if (newNbOfPokemonAPIList > NUMBER_OF_POKEMON_API_AT_STARTUP)
                    {
                        // On génère d'abord jusqu'à 50 les Pokemon depuis la base de données
                        for (var i = PokemonList.Count - nbPokemonAdded; i < NUMBER_OF_POKEMON_API_AT_STARTUP; i++)
                        {
                            pokemonFromDatabase = await App.Database._database.Table<MyPokemon>().ElementAtAsync(i);
                            PokemonList.Add(pokemonFromDatabase);
                        }
                        // On termine la génération avec des Pokemon qu'on récupère depuis l'API
                        for (var j = NUMBER_OF_POKEMON_API_AT_STARTUP; j < newNbOfPokemonAPIList; j++)
                        {
                            Pokemon pokemonfromApi = await Task.Run(() =>
                            pokeClient.GetResourceAsync<Pokemon>(j + 1));
                            sendPokemonApiToList(pokemonfromApi);
                        }
                    }
                    // Si le nombre de Pokemon de l'API qu'on souhaite est inférieur ou égal à 50
                    else
                    {
                        // On génère jusqu'au nombre voulu les Pokemon depuis la base de données
                        for (var i = PokemonList.Count - nbPokemonAdded; i < newNbOfPokemonAPIList; i++)
                        {
                            pokemonFromDatabase = await App.Database._database.Table<MyPokemon>().ElementAtAsync(i);
                            PokemonList.Add(pokemonFromDatabase);
                        }
                    }
                }
                // Si le nombre de Pokemon de l'API actuel dans la liste est supérieur ou égal à 50
                else
                {
                    // On génère jusqu'au nombre voulu des Pokemon qu'on récupère depuis l'API
                    for (var j = PokemonList.Count - nbPokemonAdded; j < newNbOfPokemonAPIList; j++)
                    {
                        Pokemon pokemonfromApi = await Task.Run(() =>
                        pokeClient.GetResourceAsync<Pokemon>(j + 1));
                        sendPokemonApiToList(pokemonfromApi);
                    }
                }
            }
        }

        // Méthode asynchrone qui permet de récupérer un Pokemon de l'API en instanciant un nouveau Pokemon de type MyPokemon,
        // de le sauvegarder en base de données s'il s'agit de la première ouverture de l'application, et de l'ajouter à la PokemonList
        // De plus, la création du Pokemon de l'API prend en compte le nombre de types et de talents que possède le Pokemon en question
        // Entrée : un Pokemon p de type Pokemon de l'API, un type relié à l'API
        public async void sendPokemonApiToList(Pokemon p)
        {
            MyPokemon newPokemonApi = new MyPokemon
            {
                Name = p.Name,
                Image = p.Sprites.FrontDefault,
                IdPoke = "N° " + p.Id,
                Type1 = getTypeImage(p.Types[0].Type.Name),
                TypeColor = getTypeColor(p.Types[0].Type.Name),
                Height = "Height : " + p.Height,
                Weight = "Weight : " + p.Weight,
                Ability1 = p.Abilities[0].Ability.Name,
                Hp = p.Stats[0].BaseStat.ToString(),
                Attack = p.Stats[1].BaseStat.ToString(),
                Defense = p.Stats[2].BaseStat.ToString(),
                SpeAttack = p.Stats[3].BaseStat.ToString(),
                SpeDefense = p.Stats[4].BaseStat.ToString(),
                Speed = p.Stats[5].BaseStat.ToString(),
                isNew = false
                };
            if (p.Types.Count == 2)
            {
                newPokemonApi.Type2 = getTypeImage(p.Types[1].Type.Name);
            }
            if (p.Abilities.Count == 2)
            {
                newPokemonApi.Ability2 = p.Abilities[1].Ability.Name;
                if (p.Abilities.Count == 3)
                {
                    newPokemonApi.Ability3 = p.Abilities[2].Ability.Name;
                }
            }
            if (nbPokemonDatabase < NUMBER_OF_POKEMON_API_AT_STARTUP)
            {
                await App.Database._database.InsertAsync(newPokemonApi);
            }
            PokemonList.Add(newPokemonApi);
        }


        // Méthode qui permet de récupérer la couleur correspondant au premier type du Pokemon en paramètre
        // Entrée : un string correspondant au premier type du Pokemon
        // Sortie : un string correspondant à la couleur HEX du type en question
        public string getTypeColor(string TypeName)
        {
            switch(TypeName)
            {
                case "grass": return "#78C850";
                case "fire": return "#F08030";
                case "water": return "#6890F0";
                case "normal": return "#A8A878";
                case "fighting": return "#C03028";
                case "bug": return "#A8B820";
                case "flying": return "#AED6F1";
                case "poison": return "#A040A0";
                case "rock": return "#B8A038";
                case "ground": return "#E0C068";
                case "steel": return "#BFC9CA";
                case "dragon": return "#7038F8";
                case "ice": return "#98D8D8";
                case "fairy": return "#EE99AC";
                case "dark": return "#35373C";
                case "ghost": return "#705898";
                case "electric": return "#F4D03F";
                case "psychic": return "#F85888";
                default: return "#FFFFFF";
            }
        }


        // Méthode qui permet de récupérer l'image correspondant au type du Pokemon en paramètre
        // Entrée : un string correspondant au type du Pokemon en paramètre
        // Sortie : un string correspondant à l'image du type du Pokemon en paramètre
        public string getTypeImage(string TypeName)
        {
            switch (TypeName)
            {
                case "grass": return "grass.png";
                case "fire": return "fire.png";
                case "water": return "water.png";
                case "normal": return "normal.png";
                case "fighting": return "fighting.png";
                case "bug": return "bug.png";
                case "flying": return "flying";
                case "poison": return "poison.png";
                case "rock": return "rock.png";
                case "ground": return "ground.png";
                case "steel": return "steel.png";
                case "dragon": return "dragon.png";
                case "ice": return "ice.png";
                case "fairy": return "fairy.png";
                case "dark": return "dark.png";
                case "ghost": return "ghost.png";
                case "electric": return "electric.png";
                case "psychic": return "psychic.png";
                default: return "normal.png";
            }
        }
    }
}
