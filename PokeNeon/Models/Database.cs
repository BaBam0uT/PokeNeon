using PokeNeon.Models;
using SQLite;

namespace PokeNeon
{
    // Cette classe contient le code permettant de créer la base de données ainsi que la table de type MyPokemon qui contiendra les 50 Pokemon de l'API et les Pokemon ajoutés par l'utilisateur
    // Le code utilise des API SQLite.NET asynchrones qui déplacent les opérations de base de données vers les threads d’arrière-plan.
    // De plus, le constructeur Database prend le chemin du fichier de base de données en tant qu’argument.
    public class Database
    {
        public readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MyPokemon>();
        }
    }
}