using System;
using StarWarsApi.Models;
using StarWarsTerminal.UI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using StarWarsTerminal.UI.Screen;
using StarWarsApi.Database;

namespace StarWarsTerminal.Main
{
    public static partial class Program
    {
        static void Main4(string[] args)
        {
            var account = LoginDarthVader();
            BuyTicket(account, 5);
            Console.ReadLine();
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