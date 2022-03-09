using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using Xamarin;
using System.Net;
using System.Text;

namespace PokeNeon.Models
{
    public class MyPokemon
    {
        public string Nom { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string TypeColor { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Ability1 { get; set; }
        public string Ability2 { get; set; }
        public string Ability3 { get; set; }
        public string Pv { get; set; }
        public string Attaque { get; set; }
        public string Defense { get; set; }
        public string Attaquespe { get; set; }
        public string Defensespe { get; set; }
        public string Vitesse { get; set; }
    }
}
