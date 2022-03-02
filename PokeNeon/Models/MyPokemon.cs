using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using Xamarin;
using System.Net;
using System.Text;

namespace PokeNeon.Models
{
    class MyPokemon
    {
        public string Nom { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string TypeColor { get; set; }
    }
}
