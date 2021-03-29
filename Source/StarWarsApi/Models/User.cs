using Newtonsoft.Json;

namespace StarWarsApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; } //Doing string because some users has "unknown" value of mass

        [JsonProperty("hair_color")] public string HairColor { get; set; }

        [JsonProperty("skin_color")] public string SkinColor { get; set; }

        [JsonProperty("eye_color")] public string EyeColor { get; set; }

        [JsonProperty("birth_year")] public string BirthYear { get; set; }

        public string Gender { get; set; }
        public string Url { get; set; }
        public Homeworld Homeplanet { get; set; }

        public class Homeworld
        {
            public int HomeworldID { get; set; }
            public string Name { get; set; }

            [JsonProperty("Rotation_Period")] public string RotationPeriod { get; set; }

            [JsonProperty("Orbital_Period")] public string OrbitalPeriod { get; set; }

            public string Diameter { get; set; }
            public string Climate { get; set; }
            public string Terrain { get; set; }
            public string Population { get; set; }
            public string Url { get; set; }
        }
    }
}