using SQLite;
using System;

namespace PokeNeon.Models
{
    // Cette classe MyPokemon contient le code permettant de définir les différentes données d'un Pokemon qu'on va utiliser dans l'application.
    // Un Pokemon de type MyPokemon possède un ID qui sert de clé primaire pour la base de données, un IdPoke qui est l'id du Pokemon dans l'API,
    // deux types divisés en deux données différentes car un Pokemon peut posséder un ou deux types, une couleur de type (exemple : le vert pour un Pokemon de type Plante),
    // une taille, un poids, trois talents divisés en trois données différentes car un Pokemon peut posséder un, deux ou trois talents,
    // des points de vie (Hp en anglais), une attaque, une défense, une attaque spéciale, une défense spéciale, une vitesse,
    // ainsi qu'un booléen isNew pour savoir si le Pokémon vient de l'API ou s'il a été ajouté par l'utilisateur
    public class MyPokemon
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string IdPoke { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string TypeColor { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Ability1 { get; set; }
        public string Ability2 { get; set; }
        public string Ability3 { get; set; }
        public string Hp { get; set; }
        public string Attack { get; set; }
        public string Defense { get; set; }
        public string SpeAttack { get; set; }
        public string SpeDefense { get; set; }
        public string Speed { get; set; }
        public Boolean isNew { get; set; }
    }
}
