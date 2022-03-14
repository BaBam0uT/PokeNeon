using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using PokeNeon.Models;
using SQLite;

namespace PokeNeon
{
    // Cette classe contient le code permettant de créer la base de données, lire les données contenues et y écrire des données.
    // Le code utilise des API SQLite.NET asynchrones qui déplacent les opérations de base de données vers les threads d’arrière-plan.
    // De plus, le constructeur Database prend le chemin du fichier de base de données en tant qu’argument.
    public class Database
    {
        public readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MyPokemon>();
            // MyPokemon poke = new MyPokemon(GetNewPokemonAsync(1));
            // Debug.WriteLine("LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOL" + poke.Nom);
        }

        public Task<List<MyPokemon>> GetMyPokemonAsync()
        {
            return _database.Table<MyPokemon>().ToListAsync();
        }

        public Task<int> SaveMyPokemonAsync(MyPokemon myPokemon)
        {
            return _database.InsertAsync(myPokemon);
        }

        public Task<int> GetCountAsync()
        {
            return _database.Table<MyPokemon>().CountAsync();
        }

        public async void GetNewPokemonAsync(int id)
        {
            MyPokemon poke = new MyPokemon();
            poke = await _database.Table<MyPokemon>().ElementAtAsync(id);
        }

    }
}