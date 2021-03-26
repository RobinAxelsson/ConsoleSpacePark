using System;
using StarWarsApi.Models;
using System.IO;
using Newtonsoft.Json;
using StarWarsApi.Database;
using StarWarsApi.Networking;

namespace StarWarsTerminal.Main
{
    public static partial class Program
    {
        static void Main(string[] args)
        {
            //CreateUser();
            var account = LoginDarthVader();
            BuyTicket(account, 5);
            Console.ReadLine();
        }
        public static Account Login(string accountName, string password)
        {
            var accountManagement = new DatabaseManagement.AccountManagement();
            var account = accountManagement.ValidateLogin(accountName, password);
            if (account == null) throw new Exception("Login didn't work");
            return account;
        }
        public static Account LoginNewUser()
        {
            string accountName = "darth123";
            string password = "darth123";
            var accountManagement = new DatabaseManagement.AccountManagement();
            var account = accountManagement.ValidateLogin(accountName, password);
            if (account == null) throw new Exception("Login didn't work");
            return account;
        }
        public static void CreateUser()
        {
            var accountName = String.Empty;
            var password1 = String.Empty;
            var password2 = String.Empty;

            Console.WriteLine("Enter user:");
            User user = null;
            do
            {
                user = APICollector.ParseUserAsync(Console.ReadLine());
                if (user == null) Console.WriteLine("user not exist, try again");
            } while (user == null);

            Console.WriteLine("user exist: " + user.Name);

            bool first = true;
            do
            {
                if (!first)
                {
                    Console.WriteLine("try again!");
                }
                Console.WriteLine("Enter accountname");
                accountName = Console.ReadLine();
                Console.WriteLine("Enter password");
                password1 = Console.ReadLine();
                Console.WriteLine("repeat password");
                password2 = Console.ReadLine();

                first = false;
            } while (password1 != password2 || accountName.Length <= 5 || password1.Length <= 5);

            string jsonstring = File.ReadAllText(@"UI/json/small-ships.json");
            var localShips = JsonConvert.DeserializeObject<SpaceShip[]>(jsonstring);
            var ship = localShips[0];
            var am = new DatabaseManagement.AccountManagement();
            am.Register(user, ship, accountName, password1);
            Console.WriteLine(user.Name + " registered successfully!");
        }
        public static void IsParkingAvailable(Account account)
        {
            var parking = new DatabaseManagement.ParkingManagement();
            var response = parking.CheckParkingStatus();
            if (response.isOpen) Console.WriteLine("Parking available");
            else Console.WriteLine("parking is available at: " + response.nextAvailable);
        }
        public static void ViewReceipts(Account account)
        {
            var am = new DatabaseManagement.AccountManagement();
            var receipts = am.GetAccountReceipts(account);
            foreach (var receipt in receipts)
            {
                string[] receiptString = new string[]
                {
                    "Ticket Holder: " + receipt.Account.AccountName,
                    "Start time: " + receipt.StartTime,
                    "End time: " + receipt.EndTime,
                    "Price: " + receipt.Price
                };
                string test = String.Join('\n', receiptString);

                Console.Write(test + '\n' + '\n');
            }
        }
        public static void BuyTicket(Account account, int hours)
        {
           
            var parking = new DatabaseManagement.ParkingManagement();
            var receipt = parking.SendInvoice(account, hours);

            string[] receiptString = new string[]
                {
                    "Ticket Holder: " + receipt.Account.AccountName,
                    "Start time: " + receipt.StartTime,
                    "End time: " + receipt.EndTime,
                    "Price: " + receipt.Price
                };
            string test = String.Join('\n', receiptString);
            Console.Write(test);
        }
        public static Account LoginDarthVader()
        {
            string accountName = "darth123";
            string password = "darth123";
            var accountManagement = new DatabaseManagement.AccountManagement();
            var account = accountManagement.ValidateLogin(accountName, password);
            if (account == null) throw new Exception("Login didn't work");
            return account;
        }
    }
}