using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApi.Models
{
    public class User
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Gender { get; set; }
        public string Eye_color { get; set; }
        public UserSpecies Species { get; set; }
        public User(string name, int id, UserSpecies species)
        {
            Name = name;
            ID = id;
            Species = species;
        }

        public class UserSpecies
        {
            public readonly string Name;
            public readonly string Classification;
            public readonly string Language;
        }
    }
}
