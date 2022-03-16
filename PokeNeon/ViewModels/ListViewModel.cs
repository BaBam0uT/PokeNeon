using PokeApiNet;
using PokeNeon.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PokeNeon.ViewModels
{
    public class ListViewModel : BaseViewModel
    {

        private static ListViewModel _instance = new ListViewModel();

        public static ListViewModel Instance { get { return _instance; } }

        private const int NB_POKEMON_API_A_AFFICHER = 50;

        public int nbrPokeDatabase = 0;

        public MyPokemon pokemon;

        public ObservableCollection<MyPokemon> ListePokemon
        {
            get { return GetValue<ObservableCollection<MyPokemon>>(); }
            set { SetValue(value); }
        }

        public ListViewModel() {
            GetList();
        }

        public async void GetList() {
            PokeApiClient pokeClient = new PokeApiClient();
            ListePokemon = new ObservableCollection<MyPokemon>();
            nbrPokeDatabase = await App.Database._database.Table<MyPokemon>().CountAsync();
            Debug.WriteLine("Nombre de Poke dans la base de données : " + nbrPokeDatabase);
            if (ListePokemon.Count == 0)
            {
                for (var i = 0; i < nbrPokeDatabase; i++)
                {
                    pokemon = await App.Database._database.Table<MyPokemon>().ElementAtAsync(i);
                    if (pokemon.isNew == true)
                    {
                        ListePokemon.Add(pokemon);
                    }
                }
                if (nbrPokeDatabase <= NB_POKEMON_API_A_AFFICHER)
                {
                    for (var j = 1; j <= NB_POKEMON_API_A_AFFICHER; j++)
                    {
                        Pokemon p = await Task.Run(() =>
                        pokeClient.GetResourceAsync<Pokemon>(j));
                        getListPokemon(p);
                    }
                }
                else
                {
                    for (var j = 0; j < NB_POKEMON_API_A_AFFICHER; j++)
                    {
                        pokemon = await App.Database._database.Table<MyPokemon>().ElementAtAsync(j);
                        ListePokemon.Add(pokemon);
                    }
                }
            }
        }

        public async void getListPokemon(Pokemon p)
        {
            if (p.Types.Count == 1 && p.Abilities.Count == 1)
            {
                MyPokemon monpoke = new MyPokemon
                {
                    Nom = p.Name,
                    Image = p.Sprites.FrontDefault,
                    IdPoke = "N° " + p.Id,
                    Type1 = getTypeImg(p.Types[0].Type.Name),
                    TypeColor = getTypeColor(p.Types[0].Type.Name),
                    Height = "Height : " + p.Height,
                    Weight = "Weight : " + p.Weight,
                    Ability1 = p.Abilities[0].Ability.Name,
                    Pv = p.Stats[0].BaseStat.ToString(),
                    Attaque = p.Stats[1].BaseStat.ToString(),
                    Defense = p.Stats[2].BaseStat.ToString(),
                    Attaquespe = p.Stats[3].BaseStat.ToString(),
                    Defensespe = p.Stats[4].BaseStat.ToString(),
                    Vitesse = p.Stats[5].BaseStat.ToString(),
                    isNew = false
                };
                if (nbrPokeDatabase <= NB_POKEMON_API_A_AFFICHER)
                {
                    await App.Database._database.InsertAsync(monpoke);
                }
                ListePokemon.Add(monpoke);
            }
            else if (p.Types.Count == 1 && p.Abilities.Count == 2)
            {
                MyPokemon monpoke = new MyPokemon
                {
                    Nom = p.Name,
                    Image = p.Sprites.FrontDefault,
                    IdPoke = "N° " + p.Id,
                    Type1 = getTypeImg(p.Types[0].Type.Name),
                    TypeColor = getTypeColor(p.Types[0].Type.Name),
                    Height = "Height : " + p.Height,
                    Weight = "Weight : " + p.Weight,
                    Ability1 = p.Abilities[0].Ability.Name,
                    Ability2 = p.Abilities[1].Ability.Name,
                    Pv = p.Stats[0].BaseStat.ToString(),
                    Attaque = p.Stats[1].BaseStat.ToString(),
                    Defense = p.Stats[2].BaseStat.ToString(),
                    Attaquespe = p.Stats[3].BaseStat.ToString(),
                    Defensespe = p.Stats[4].BaseStat.ToString(),
                    Vitesse = p.Stats[5].BaseStat.ToString(),
                    isNew = false
                };
                if (nbrPokeDatabase <= NB_POKEMON_API_A_AFFICHER)
                {
                    await App.Database._database.InsertAsync(monpoke);
                }
                ListePokemon.Add(monpoke);
            }
            else if (p.Types.Count == 1 && p.Abilities.Count == 3)
            {
                MyPokemon monpoke = new MyPokemon
                {
                    Nom = p.Name,
                    Image = p.Sprites.FrontDefault,
                    IdPoke = "N° " + p.Id,
                    Type1 = getTypeImg(p.Types[0].Type.Name),
                    TypeColor = getTypeColor(p.Types[0].Type.Name),
                    Height = "Height : " + p.Height,
                    Weight = "Weight : " + p.Weight,
                    Ability1 = p.Abilities[0].Ability.Name,
                    Ability2 = p.Abilities[1].Ability.Name,
                    Ability3 = p.Abilities[2].Ability.Name,
                    Pv = p.Stats[0].BaseStat.ToString(),
                    Attaque = p.Stats[1].BaseStat.ToString(),
                    Defense = p.Stats[2].BaseStat.ToString(),
                    Attaquespe = p.Stats[3].BaseStat.ToString(),
                    Defensespe = p.Stats[4].BaseStat.ToString(),
                    Vitesse = p.Stats[5].BaseStat.ToString(),
                    isNew = false
                };
                if (nbrPokeDatabase <= NB_POKEMON_API_A_AFFICHER)
                {
                    await App.Database._database.InsertAsync(monpoke);
                }
                ListePokemon.Add(monpoke);
            }
            else if (p.Types.Count == 2 && p.Abilities.Count == 1)
            {
                MyPokemon monpoke = new MyPokemon
                {
                    Nom = p.Name,
                    Image = p.Sprites.FrontDefault,
                    IdPoke = "N° " + p.Id,
                    Type1 = getTypeImg(p.Types[0].Type.Name),
                    Type2 = getTypeImg(p.Types[1].Type.Name),
                    TypeColor = getTypeColor(p.Types[0].Type.Name),
                    Height = "Height : " + p.Height,
                    Weight = "Weight : " + p.Weight,
                    Ability1 = p.Abilities[0].Ability.Name,
                    Pv = p.Stats[0].BaseStat.ToString(),
                    Attaque = p.Stats[1].BaseStat.ToString(),
                    Defense = p.Stats[2].BaseStat.ToString(),
                    Attaquespe = p.Stats[3].BaseStat.ToString(),
                    Defensespe = p.Stats[4].BaseStat.ToString(),
                    Vitesse = p.Stats[5].BaseStat.ToString(),
                    isNew = false
                };
                if (nbrPokeDatabase <= NB_POKEMON_API_A_AFFICHER)
                {
                    await App.Database._database.InsertAsync(monpoke);
                }
                ListePokemon.Add(monpoke);
            }
            else if (p.Types.Count == 2 && p.Abilities.Count == 2)
            {
                MyPokemon monpoke = new MyPokemon
                {
                    Nom = p.Name,
                    Image = p.Sprites.FrontDefault,
                    IdPoke = "N° " + p.Id,
                    Type1 = getTypeImg(p.Types[0].Type.Name),
                    Type2 = getTypeImg(p.Types[1].Type.Name),
                    TypeColor = getTypeColor(p.Types[0].Type.Name),
                    Height = "Height : " + p.Height,
                    Weight = "Weight : " + p.Weight,
                    Ability1 = p.Abilities[0].Ability.Name,
                    Ability2 = p.Abilities[1].Ability.Name,
                    Pv = p.Stats[0].BaseStat.ToString(),
                    Attaque = p.Stats[1].BaseStat.ToString(),
                    Defense = p.Stats[2].BaseStat.ToString(),
                    Attaquespe = p.Stats[3].BaseStat.ToString(),
                    Defensespe = p.Stats[4].BaseStat.ToString(),
                    Vitesse = p.Stats[5].BaseStat.ToString(),
                    isNew = false
                };
                if (nbrPokeDatabase <= NB_POKEMON_API_A_AFFICHER)
                {
                    await App.Database._database.InsertAsync(monpoke);
                }
                ListePokemon.Add(monpoke);
            }
            else
            {
                MyPokemon monpoke = new MyPokemon
                {
                    Nom = p.Name,
                    Image = p.Sprites.FrontDefault,
                    IdPoke = "N° " + p.Id,
                    Type1 = getTypeImg(p.Types[0].Type.Name),
                    Type2 = getTypeImg(p.Types[1].Type.Name),
                    TypeColor = getTypeColor(p.Types[0].Type.Name),
                    Height = "Height : " + p.Height,
                    Weight = "Weight : " + p.Weight,
                    Ability1 = p.Abilities[0].Ability.Name,
                    Ability2 = p.Abilities[1].Ability.Name,
                    Ability3 = p.Abilities[2].Ability.Name,
                    Pv = p.Stats[0].BaseStat.ToString(),
                    Attaque = p.Stats[1].BaseStat.ToString(),
                    Defense = p.Stats[2].BaseStat.ToString(),
                    Attaquespe = p.Stats[3].BaseStat.ToString(),
                    Defensespe = p.Stats[4].BaseStat.ToString(),
                    Vitesse = p.Stats[5].BaseStat.ToString(),
                    isNew = false
                };
                if (nbrPokeDatabase <= NB_POKEMON_API_A_AFFICHER)
                {
                    await App.Database._database.InsertAsync(monpoke);
                }
                ListePokemon.Add(monpoke);
            }
        }

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
                case "flying": return "##AED6F1";
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

        public string getTypeImg(string TypeName)
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
