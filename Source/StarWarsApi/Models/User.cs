using Newtonsoft.Json;

namespace StarWarsApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; } //Doing string because some users has "unknown" value of mass
        [JsonProperty("hair_color")]
        public string hairColor { get; set; }
        [JsonProperty("skin_color")]
        public string skinColor { get; set; }
        [JsonProperty("eye_color")]
        public string eyeColor { get; set; }
        [JsonProperty("birth_year")]
        public string birthYear { get; set; }
        public string Gender { get; set; }
        public string URL { get; set; }
        public Homeworld Homeplanet { get; set; }
        public int StarWarsID { get; set; } //The ID in the URL used to parse the person in mind. Used to store data in our local database
        public class Homeworld
        {
            public string Name { get; set; }
            public string RotationPeriod { get; set; }
            public string Orbital_Period { get; set; }
            public string Diameter { get; set; }
            public string Climate { get; set; }
            public string Terrain { get; set; }
            public string Population { get; set; }
            public string URL { get; set; }
        }
    }
}
