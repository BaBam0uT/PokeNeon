using PokeApiNet;
using PokeNeon.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PokeNeon.ViewModels
{
    class ListViewModel : BaseViewModel
    {
        private static ListViewModel _instance = new ListViewModel();

        public static ListViewModel Instance { get { return _instance; } }

        public ObservableCollection<MyPokemon> ListePokemons
        {
            get { return GetValue<ObservableCollection<MyPokemon>>(); }
            set { SetValue(value); }
        }

        public ListViewModel() {
            GetList();
        }

        public async void GetList() {
            ListePokemons = new ObservableCollection<MyPokemon>();
            PokeApiClient pokeClient = new PokeApiClient();
            if (ListePokemons.Count == 0)
            {
                for (var i = 1; i <= 151; i++)
                {
                    Pokemon p = await Task.Run(() =>
                    pokeClient.GetResourceAsync<Pokemon>(i));

                    if (p.Types.Count == 1)
                    {
                        MyPokemon monpoke = new MyPokemon
                        {
                            Nom = p.Name,
                            Image = p.Sprites.FrontDefault,
                            Id = "N° " + p.Id,
                            Type1 = getTypeImg(p.Types[0].Type.Name),
                            TypeColor = getTypeColor(p.Types[0].Type.Name)
                        };
                        ListePokemons.Add(monpoke);
                    }
                    else
                    {
                        MyPokemon monpoke = new MyPokemon
                        {
                            Nom = p.Name,
                            Image = p.Sprites.FrontDefault,
                            Id = "N° " + p.Id,
                            Type1 = getTypeImg(p.Types[0].Type.Name),
                            Type2 = getTypeImg(p.Types[1].Type.Name),
                            TypeColor = getTypeColor(p.Types[0].Type.Name)
                        };
                        ListePokemons.Add(monpoke);
                    }
                }
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
