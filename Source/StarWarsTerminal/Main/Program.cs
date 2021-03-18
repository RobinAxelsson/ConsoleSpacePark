using System;
using StarWarsApi.Models;
using StarWarsApi.Repository;
namespace StarWarsTerminal.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceShip spaceShip = new SpaceShip();
            spaceShip.Size = new SpaceShip.ShipSize(30, 100, 20);
            User user = APICollector.ParseUser("https://swapi.dev/api/people/1/");
            Console.WriteLine($"Eye color: {user.Eye_color}\nStarWarsID: {user.ID}");
        }
    }
}