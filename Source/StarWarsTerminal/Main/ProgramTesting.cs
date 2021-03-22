using System;
using StarWarsApi.Models;
using StarWarsApi.Networking;

namespace StarWarsTerminal.Main
{
    public static class ProgramTesting
    {
        private static void Main(string[] args)
        {
            var spaceShips = APICollector.ReturnShipAsync();
            foreach (var e in spaceShips)
            {
                Console.WriteLine(e.Name);
            }
        }
    }
}