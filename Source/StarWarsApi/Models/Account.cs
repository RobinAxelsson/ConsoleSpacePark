using Newtonsoft.Json;
namespace StarWarsApi.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public User User { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
    }
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
        public class Homeworld
        {
            public int HomeworldID { get; set; }
            public string Name { get; set; }
            [JsonProperty("Rotation_Period")]
            public string RotationPeriod { get; set; }
            [JsonProperty("Orbital_Period")]
            public string OrbitalPeriod { get; set; }
            public string Diameter { get; set; }
            public string Climate { get; set; }
            public string Terrain { get; set; }
            public string Population { get; set; }
            public string URL { get; set; }
        }
    }
}