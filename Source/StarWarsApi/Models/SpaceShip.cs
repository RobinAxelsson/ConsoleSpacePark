using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApi.Models
{
    public class SpaceShip
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public ShipSize Size { get; set; } 
        //Noticed SWAPI JSON doesn't contain values for height or width. Will we use our own measurements?
        public class ShipSize
        {
            public readonly long Width;
            public readonly long Length;
            public readonly long Height;
            public ShipSize(long width, long length, long height)
            {
                Width = width;
                Length = length;
                Height = height;
            }
        }
    }
}
