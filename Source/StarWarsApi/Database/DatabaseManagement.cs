using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models;
using StarWarsApi.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace StarWarsApi.Database
{
    public class DatabaseManagement
    {
        public static double PriceMultiplier = 10;
        public static int ParkingSlots = 5;
        private static string ConnectionString { get; set; }

        public static void SetConnectionString(string filePath) { ConnectionString = File.ReadAllText(filePath); }

        public class ParkingManagement
        {
            private ParkingManagement() //Instantiation. This will help us throw if ConnectionString is null.
            {
                if (ConnectionString == null)
                    throw new Exception("The static property ConnectionString has not been assigned.",
                        new Exception(
                            "Please assign a value to the static property ConnectionString before calling any methods"));
            }

            #region Static Methods

            //These methods instantiate ParkingManagement and call upon the private non-static methods.
            public static (bool isOpen, DateTime nextAvailable) CheckParkingStatus()
            {
                var pm = new ParkingManagement();
                return pm._CheckParkingStatus();
            }

            public static decimal CalculatePrice(SpaceShip ship, double minutes)
            {
                var pm = new ParkingManagement();
                return pm._CalculatePrice(ship, minutes);
            }

            public static Receipt SendInvoice(Account account, double minutes)
            {
                var pm = new ParkingManagement();
                return pm._SendInvoice(account, minutes);
            }

            #endregion

            #region Instantiated Methods

            private (bool isOpen, DateTime nextAvailable) _CheckParkingStatus()
            {
                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                var ongoingParkings = new List<Receipt>();
                foreach (var receipts in dbHandler.Receipts)
                    if (DateTime.Parse(receipts.EndTime) > DateTime.Now)
                        ongoingParkings.Add(receipts);
                var nextAvailable = DateTime.Now;
                var isOpen = false;
                if (ongoingParkings.Count >= ParkingSlots)
                {
                    //Setting nextAvailable 10 years ahead so the loop will always start running.
                    nextAvailable = DateTime.Now.AddYears(10);
                    var cachedNow = DateTime.Now;
                    //Caching DateTimeNow in case loops takes longer than expected, to ensure that time moving forward doesn't break the loop.
                    foreach (var receipt in ongoingParkings)
                    {
                        var endTime = DateTime.Parse(receipt.EndTime);
                        if (endTime > cachedNow && endTime < nextAvailable) nextAvailable = endTime;
                    }
                }
                else
                {
                    isOpen = true;
                }

                return (isOpen, nextAvailable);
            }

            private decimal _CalculatePrice(SpaceShip ship, double minutes)
            {
                var price = double.Parse(ship.ShipLength.Replace(".", ",")) * minutes / PriceMultiplier;
                return (decimal)price;
            }

            private Receipt _SendInvoice(Account account, double minutes)
            {
                var receipt = new Receipt();
                var thread = new Thread(() => { receipt = Execution_SendInvoice(account, minutes); });
                thread.Start();
                thread.Join(); //By doing join it will wait for the method to finish
                return receipt;
            }

            private Receipt Execution_SendInvoice(Account account, double minutes)
            {
                var price = _CalculatePrice(account.SpaceShip, minutes);
                var endTime = DateTime.Now.AddMinutes(minutes);
                var receipt = new Receipt
                {
                    Account = account,
                    Price = price,
                    StartTime = DateTime.Now.ToString("g"),
                    EndTime = endTime.ToString("g")
                };
                if (ConnectionString == null)
                    throw new Exception("The static property ConnectionString has not been assigned.",
                        new Exception(
                            "Please assign a value to the static property ConnectionString before calling any methods"));
                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                dbHandler.Receipts.Update(receipt);
                dbHandler.SaveChanges(); //TODO
                return receipt;
            }

            #endregion
        }

        public class AccountManagement
        {
            private AccountManagement()
            {
                if (ConnectionString == null)
                    throw new Exception("The static property ConnectionString has not been assigned.",
                        new Exception(
                            "Please assign a value to the static property ConnectionString before calling any methods"));
            } //Instantiation. This will help us throw if ConnectionString is null.
            #region Instantiated Methods

            #region Overloads

            private bool _Exists(User inputUser)
            {
                var result = false;
                var thread = new Thread(() => { result = Execution_Exists(inputUser); });
                thread.Start();
                thread.Join(); //By doing join it will wait for the method to finish
                return result;
            }

            private List<Receipt> GetAccountReceipts(string accountName)
            {
                var receiptList = new List<Receipt>();
                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                foreach (var receipt in dbHandler.Receipts)
                    if (receipt.Account.AccountName == accountName)
                        receiptList.Add(receipt);
                return receiptList;
            }

            private List<Receipt> GetAccountReceipts(int accountId)
            {
                var receiptList = new List<Receipt>();
                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                foreach (var receipt in dbHandler.Receipts)
                    if (receipt.Account.AccountID == accountId)
                        receiptList.Add(receipt);
                return receiptList;
            }

            private bool Execution_Exists(User inputUser)
            {
                var result = false;
                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                foreach (var user in dbHandler.Users)
                    if (user.Name == inputUser.Name)
                        result = true;

                return result;
            }

            #endregion

            //Async method. Below will call upon a private corresponding method in another thread.
            private void _Register(User inputUser, SpaceShip inputShip, string accountName, string password)
            {
                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                var outputAccount = new Account
                {
                    AccountName = accountName,
                    Password = PasswordHashing.HashPassword(password),
                    User = inputUser,
                    SpaceShip = inputShip
                };
                inputUser.Homeplanet = dbHandler.Homeworlds.FirstOrDefault(g => g.Name == inputUser.Homeplanet.Name) ??
                                       inputUser.Homeplanet;
                dbHandler.Accounts.Add(outputAccount);
                dbHandler.SaveChanges();
            }

            private List<Receipt> _GetAccountReceipts(Account account)
            {
                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                return dbHandler.Receipts.Include(a => a.Account)
                    .Where(receipt => receipt.Account.AccountName == account.AccountName).ToList();
            }

            //Async method. Below will call upon a private corresponding method in another thread.
            private bool _Exists(string name, bool isAccountName)
            {
                var result = false;
                var thread = new Thread(() => { result = Execution_Exists(name, isAccountName); });
                thread.Start();
                thread.Join(); //By doing join it will wait for the method to finish
                return result;
            }

            //Execution method that will do the "work"
            private bool Execution_Exists(string name, bool isAccountName)
            {
                if (isAccountName)
                {
                    var result = false;
                    var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                    foreach (var account in dbHandler.Accounts)
                        if (account.AccountName == name)
                            result = true;

                    return result;
                }
                else
                {
                    var result = false;
                    var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                    foreach (var account in dbHandler.Users)
                        if (account.Name == name)
                            result = true;

                    return result;
                }
            }

            private Account _ValidateLogin(string accountName, string passwordInput)
            {
                var account = new Account();
                var thread = new Thread(() =>
                {
                    account = Execution_ValidateLogin(accountName, PasswordHashing.HashPassword(passwordInput));
                });
                thread.Start();
                thread.Join(); //By doing join it will wait for the method to finish
                return account;
            }

            private Account Execution_ValidateLogin(string accountName, string passwordInput)
            {
                Account accountHolder = null;
                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                foreach (var account in dbHandler.Accounts.Include(a => a.User).Include(a => a.SpaceShip)
                    .Include(h => h.User.Homeplanet))
                    if (account.AccountName == accountName && account.Password == passwordInput)
                        accountHolder = account;
                return accountHolder;
            }

            #endregion

            #region Static Methods

            #region Private Methods

            private static (string question, string answer) GetSecurityQuestion(User inputUser)
            {
                var question = string.Empty;
                var answer = string.Empty;
                var r = new Random();
                var x = r.Next(1, 4);
                switch (x)
                {
                    case 1:
                        question = "What is your hair color?";
                        answer = inputUser.HairColor;
                        break;
                    case 2:
                        question = "What is your skin color?";
                        answer = inputUser.SkinColor;
                        break;
                    case 3:
                        question = "What is your eye color?";
                        answer = inputUser.EyeColor;
                        break;
                    case 4:
                        question = "What is your birth year?";
                        answer = inputUser.BirthYear;
                        break;
                }

                return (question, answer);
            }

            #endregion

            #region Public Methods & IEnumerables

            //These methods instantiate AccountManagement and call upon the private non-static methods.
            public static IEnumerable<Receipt> GetAccountReceipts(Account account)
            {
                var am = new AccountManagement();
                return am._GetAccountReceipts(account);
            }

            public static Account ValidateLogin(string accountName, string passwordInput)
            {
                var am = new AccountManagement();
                return am._ValidateLogin(accountName, passwordInput);
            }

            public static void Register(User inputUser, SpaceShip inputShip, string accountName, string password)
            {
                var am = new AccountManagement();
                am._Register(inputUser, inputShip, accountName, password);
            }

            public static void ReRegisterShip(Account account, SpaceShip ship)
            {
                ship.SpaceShipID = account.SpaceShip.SpaceShipID;
                account.SpaceShip = ship;

                var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                dbHandler.SpaceShips.Update(ship);
                dbHandler.SaveChanges();
            }

            public static User IdentifyWithQuestion(string username, Func<string, string> getSecurityAnswer)
            {
                var inputUser = APICollector.ParseUserAsync(username);
                if (inputUser == null) return null;

                var (question, answer) = GetSecurityQuestion(inputUser);
                var inputAnswer = getSecurityAnswer(question);
                if (inputAnswer.ToLower() == answer.ToLower()) return inputUser;
                return null;
            }

            public static bool Exists(string name, bool isAccountName)
            {
                var am = new AccountManagement();
                return am._Exists(name, isAccountName);
            }
            
            #region Overloads
            public static bool Exists(User user)
            {
                var am = new AccountManagement();
                return am._Exists(user);
            }
            #endregion
            
            
            #endregion

            #endregion
            private static class PasswordHashing
            {
                private const string Salt = "78378265240709988066";

                //Salting password to enhance security by adding data to the password before the SHA256 conversion.
                //This makes it more difficult(impossible) to datamine.
                public static string HashPassword(string input)
                {
                    var hashedData = ComputeSha256Hash(input + Salt);
                    return hashedData;
                }

                private static string ComputeSha256Hash(string rawData)
                {
                    using var sha256Hash = SHA256.Create();
                    var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData + Salt));
                    var builder = new StringBuilder();
                    foreach (var t in bytes)
                        builder.Append(t.ToString("x2"));
                    return builder.ToString();
                }
            }

       
        }
    }
}