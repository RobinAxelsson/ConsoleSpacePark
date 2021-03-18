using System;
using StarWarsApi.Models;
using StarWarsApi.Repository;
using StarWarsApi.Database;
namespace StarWarsTerminal.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            StarWarsContext context = new StarWarsContext();
            var user = new User("Albin", 4);
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}