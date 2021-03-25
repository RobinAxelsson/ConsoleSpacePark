using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models;
using StarWarsApi.Networking;

namespace StarWarsApi.Database
{
    public class DatabaseManagement
    {
        public static double PriceMultiplier = 10;
        public static string ConnectionString { get; set; }
        public static int ParkingSlots = 5;
        public class ParkingManagement
        {
            //Checks if space park is open or closed, if closed, when is nextAvailable slot?
            public (bool isOpen, DateTime nextAvailable) CheckParkingStatus()
            {
                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                var ongoingParkings = dbHandler.Receipts.Where(r => DateTime.Parse(r.EndTime) > DateTime.Now).ToList();
                var nextAvailable = DateTime.Now;
                var isOpen = false;
                if(ongoingParkings.Count >= ParkingSlots)
                {
                    //Setting nextAvailable 10 years ahead so the loop will always start running.
                    nextAvailable = DateTime.Now.AddYears(10);
                    var cachedNow = DateTime.Now; 
                    //Caching DateTimeNow in case loops takes longer than expected, to ensure that time moving forward doesn't break the loop.
                    foreach (var receipt in ongoingParkings)
                    {
                        var endTime = DateTime.Parse(receipt.EndTime);
                        if (endTime > cachedNow && endTime < nextAvailable)
                        {
                            nextAvailable = endTime;
                        }
                    }
                }
                else
                {
                    isOpen = true;
                }
                return (isOpen, nextAvailable);
            }
            public double CalculatePrice(SpaceShip ship, double minutes)
            {
                var price = (double.Parse(ship.ShipLength.Replace(".",",")) * minutes) / PriceMultiplier;
                return price;   
            }
            public Receipt SendInvoice(Account account, double minutes)
            {
                var receipt = new Receipt();
                var thread = new Thread(() => { receipt = sendInvoice(account, minutes); });
                thread.Start();
                thread.Join(); //By doing join it will wait for the method to finish
                return receipt;
            }
            private Receipt sendInvoice(Account account, double minutes)
            {
                var price = CalculatePrice(account.SpaceShip, minutes);
                var endTime = DateTime.Now.AddMinutes(minutes);
                var receipt = new Receipt
                {
                    Account = account, Price = price, StartTime = DateTime.Now.ToString("g"), EndTime = endTime.ToString("g")
                };
                if (ConnectionString == null)
                    throw new Exception("The static property ConnectionString has not been assigned.",
                        new Exception(
                            "Please assign a value to the static property ConnectionString before calling any methods"));
                var dbHandler = new StarWarsContext {ConnectionString = ConnectionString};
                dbHandler.Receipts.Add(receipt);
                dbHandler.SaveChanges();
                return receipt;
            }
        }
        public class AccountManagement
        {
            public AccountManagement()
            {
                if (ConnectionString == null)
                    throw new Exception("The static property ConnectionString has not been assigned.",
                        new Exception(
                            "Please assign a value to the static property ConnectionString before calling any methods"));
            }

            public void Register(User inputUser, SpaceShip inputShip, string accountName, string password)
            {
                var dbHandler = new StarWarsContext {ConnectionString = ConnectionString};
                var outputAccount = new Account
                {
                    AccountName = accountName, Password = PasswordHashing.HashPassword(password), User = inputUser,
                    SpaceShip = inputShip
                };
                inputUser.Homeplanet = dbHandler.Homeworlds.FirstOrDefault(g => g.Name == inputUser.Homeplanet.Name) ??
                                       inputUser.Homeplanet;
                dbHandler.Accounts.Add(outputAccount);
                dbHandler.SaveChanges();
            }
            public bool Exists(User inputUser)
            {
                var result = false;
                var thread = new Thread(() => { result = exists(inputUser); });
                thread.Start();
                thread.Join(); //By doing join it will wait for the method to finish
                return result;
            }
            private bool exists(User inputUser)
            {
                var result = false;
                var dbHandler = new StarWarsContext {ConnectionString = ConnectionString};
                foreach (var user in dbHandler.Users)
                    if (user.Name == inputUser.Name)
                        result = true;

                return result;
            }
            public Account ValidateLogin(string accountName, string passwordInput)
            {
                var account = new Account();
                var thread = new Thread(() =>
                {
                    account = validateLogin(accountName, PasswordHashing.HashPassword(passwordInput));
                });
                thread.Start();
                thread.Join(); //By doing join it will wait for the method to finish
                return account;
            }
            private Account validateLogin(string accountName, string passwordInput)
            {
                var dbHandler = new StarWarsContext {ConnectionString = ConnectionString};
                return dbHandler.Accounts.Where(a => a.AccountName == accountName && a.Password == passwordInput).Include(a => a.User).Include(a => a.SpaceShip).Single();
            }
            private List<Receipt> GetAccountReceipts(string accountName)
            {
                var receiptList = new List<Receipt>();
                var dbHandler = new StarWarsContext {ConnectionString = ConnectionString};
                foreach (var receipt in dbHandler.Receipts)
                {
                    if (receipt.Account.AccountName == accountName)
                    {
                        receiptList.Add(receipt);
                    }
                }
                return receiptList;
            }
            private List<Receipt> GetAccountReceipts(int accountId)
            {
                var receiptList = new List<Receipt>();
                var dbHandler = new StarWarsContext {ConnectionString = ConnectionString};
                foreach (var receipt in dbHandler.Receipts)
                {
                    if (receipt.Account.AccountID == accountId)
                    {
                        receiptList.Add(receipt);
                    }
                }
                return receiptList;
            }
            
            private static Account testAccount = new Account();

            public static bool IdentifyWithQuestion(string username, Func<string, string> getSecurityAnswer)
            {
                var inputUser = APICollector.ParseUserAsync(username);
                var security = DatabaseManagement.AccountManagement.GetSecurityQuestion(inputUser);
                var inputAnswer = getSecurityAnswer(security.question);
                if (inputAnswer.ToLower() == security.answer.ToLower())
                {
                    testAccount.User = inputUser;
                    return true;
                }
                else
                    return false;
            }
            public static bool IsRegistrated()
            {
                var am = new DatabaseManagement.AccountManagement();

                return am.Exists(testAccount.User);
            }
            public static void Register(SpaceShip ship, Func<(string accountName, string password)> registerScreen)
            {
                //testAccount add ship
                var userinput = registerScreen();
                var userName = userinput.accountName;
                var password = userinput.password;
                var am = new DatabaseManagement.AccountManagement();
                am.Register(testAccount.User, ship, userName, password); //add spaceship
            }
            public static (string question, string answer) GetSecurityQuestion(User inputUser)
            {
                var question = "Vad är Calles favoriträtt?";
                var _answer = "Svar: Svensk husmanskost";
                var r = new Random();
                var x = r.Next(1, 4);
                switch (x)
                {
                    case 1:
                        question = "What is your hair color?";
                        _answer = inputUser.hairColor;
                        break;
                    case 2:
                        question = "What is your skin color?";
                        _answer = inputUser.skinColor;
                        break;
                    case 3:
                        question = "What is your eye color?";
                        _answer = inputUser.eyeColor;
                        break;
                    case 4:
                        question = "What is your birth year?";
                        _answer = inputUser.birthYear;
                        break;
                }

                return (question, _answer);
            }

            private static class PasswordHashing
            {
                private const string salt = "78378265240709988066";

                //Salting password to enhance security by adding data to the password before the SHA256 conversion.
                //This makes it more difficult(impossible) to datamine.
                public static string HashPassword(string input)
                {
                    var hashedData = ComputeSha256Hash(input + salt);
                    return hashedData;
                }

                private static string ComputeSha256Hash(string rawData)
                {
                    using var sha256Hash = SHA256.Create();
                    var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData + salt));
                    var builder = new StringBuilder();
                    foreach (var t in bytes)
                        builder.Append(t.ToString("x2"));
                    return builder.ToString();
                }
            }
        }
    }
}