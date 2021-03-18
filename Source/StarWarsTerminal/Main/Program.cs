using System;
using StarWarsApi.Models;
namespace StarWarsTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceShip spaceShip = new SpaceShip();
            spaceShip.Size = new SpaceShip.ShipSize(50, 200, 20);
            Console.WriteLine(
                $"Width: {spaceShip.Size.Width}\nLength: {spaceShip.Size.Length}\nHeight: {spaceShip.Size.Height}");
        }
    }
}