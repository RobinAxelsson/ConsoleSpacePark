using System;
using System.Collections.Generic;
using System.Linq;
using StarWarsApi.Database;
using StarWarsApi.Models;
using StarWarsApi.Networking;

namespace StarWarsTerminal.Main
{
    public static class ProgramTesting
    {

        private static void Main2(string[] args)
        {
            /*
            var deathstar = APICollector.ParseShip("Death Star");
            Console.WriteLine($"Death star size: {deathstar.Price}");
            var spaceships = APICollector.ReturnShipsAsync();
            List<long> spaceship_lengths = new List<long>();
            foreach (var ship in spaceships)
            {
                if(long.Parse(ship.ShipLength) < 500)
                    spaceship_lengths.Add(long.Parse(ship.ShipLength));

                }

            spaceship_lengths.Sort();
            foreach (var item in spaceship_lengths)
            {
                Console.WriteLine(item);
            }
            */
        }
    }
}