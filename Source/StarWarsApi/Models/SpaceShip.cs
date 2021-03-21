using Newtonsoft.Json;
using StarWarsApi.Interfaces;

namespace StarWarsApi.Models
{
    public class SpaceShip : IStarwarsItem
    {
        public int SpaceShipID { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        [JsonProperty("cost_in_credits")]
        public string Price { get; set; }
        [JsonProperty("starship_class")]
        public string Classification { get; set; }
        private string Length { get; set; }
        public string URL { get; set; }
        public ShipSize Size { get; set; } 
        //Noticed SWAPI JSON doesn't contain values for height or width. Will we use our own measurements?
        public class ShipSize
        {
            public int ShipSizeID { get; set; }
            public long Width { get; private set; }
            public long Length { get; private set; }
            public long Height { get; private set; }
            public ShipSize(long width, long length, long height)
            {
                Width = width;
                Length = length;
                Height = height;
            }
            public ShipSize()
            {

            }
        }
    }
}
