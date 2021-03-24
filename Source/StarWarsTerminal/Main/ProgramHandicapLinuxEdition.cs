using System;
using StarWarsApi.Database;
using StarWarsApi.Networking;

namespace StarWarsTerminal.Main
{
    static class ProgramHandicapLinuxEdition
    {
        static void Main2(string[] args)
        {
            Console.WriteLine("Please enter your name:");
            var username = Console.ReadLine();
            var inputUser = APICollector.ParseUserAsync(username);
            //Loading screen "Looking up user in database..."
            var security = DatabaseManagement.AccountManagement.GetSecurityQuestion(inputUser);
            Console.WriteLine(security.question);
            var inputAnswer = Console.ReadLine();
            if (inputAnswer.ToLower() == security.answer.ToLower())
            {
                Console.WriteLine("Correct!"); 
                DatabaseManagement.ConnectionString = @"Server=90.229.161.68,52578;Database=StarWarsProject2.1;User Id=adminuser;Password=starwars;";
                var am = new DatabaseManagement.AccountManagement();
                if (!am.Exists(inputUser))
                {
                    Console.WriteLine("Please enter your desired accountname: ");
                    var accountName = Console.ReadLine();
                    Console.WriteLine("Please enter your desired password:");
                    var password = Console.ReadLine();
                    //am.Register(inputUser, accountName, password);
                    Console.WriteLine("Successfully Registered!");
                    //Load register screen which contains only Account Name and Password
                    //Input userObject, string accountName, string password > Register method
                    //Then please login
                }
                else
                {
                    Console.WriteLine("This user is already registered!");
                }
               
            }
        }
    }
}