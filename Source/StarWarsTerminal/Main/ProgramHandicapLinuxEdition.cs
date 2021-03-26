using System;
using StarWarsApi.Database;
using StarWarsApi.Models;
using StarWarsApi.Networking;

namespace StarWarsTerminal.Main
{
    static class ProgramHandicapLinuxEdition
    {
        static void Main(string[] args)
        {
        //    DatabaseManagement.ConnectionString = @"Server = 90.229.161.68,52578; Database = StarWarsProject2.6; User Id = adminuser; Password = starwars;";
      //  Console.WriteLine(DatabaseManagement.AccountManagement.DoExists("darth123"));
        }
        public static void ViewReceipts(Account account)
        {
            var receipts = DatabaseManagement.AccountManagement.GetAccountReceipts(account);
            foreach (var receipt in receipts)
            {
                string[] receiptString = new string[]
                {
                    "Ticket Holder: " + receipt.Account.AccountName,
                    "Start time: " + receipt.StartTime,
                    "End time: " + receipt.EndTime,
                    "Price: " + receipt.Price
                };
                var test = String.Join('\n', receiptString);

                Console.Write(test + '\n' + '\n');
            }
        }
        public static void BuyTicket(Account account, int hours)
        {
            var receipt = DatabaseManagement.ParkingManagement.SendInvoice(account, hours);

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
            var account = DatabaseManagement.AccountManagement.ValidateLogin(accountName, password);
            if (account == null) throw new Exception("Login didn't work");
            return account;
        }
    }
}