using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using StarWarsApi.Models;
using StarWarsApi.Networking;

namespace StarWarsApi.Database
{
    public class DatabaseManagement
    {
        public static string ConnectionString { get; set; }
        public class AccountManagement
        {
            public AccountManagement()
            {
                if (ConnectionString == null)
                    throw new Exception("The static property ConnectionString has not been assigned.",
                        new Exception(
                            "Please assign a value to the static property ConnectionString before calling any methods"));
            }

            public void Register(User inputUser, string accountName, string password)
            {
                var dbHandler = new StarWarsContext {ConnectionString = ConnectionString};
                var outputAccount = new Account
                {
                    AccountName = accountName, Password = PasswordHashing.HashPassword(password), User = inputUser
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
            public bool ValidateLogin(string accountName, string passwordInput)
            {
                var result = false;
                var thread = new Thread(() =>
                {
                    result = validateLogin(accountName, PasswordHashing.HashPassword(passwordInput));
                });
                thread.Start();
                thread.Join(); //By doing join it will wait for the method to finish
                return result;
            }
            private bool validateLogin(string accountName, string passwordInput)
            {
                var result = false;
                var dbHandler = new StarWarsContext {ConnectionString = ConnectionString};
                foreach (var account in dbHandler.Accounts)
                    if (account.AccountName == accountName)
                        if (account.Password == passwordInput)
                            result = true;
                return result; //return user account?
            }

            private static Account testAccount;
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
                DatabaseManagement.ConnectionString = @"Server=90.229.161.68,52578;Database=StarWarsProject2.1;User Id=adminuser;Password=starwars;";
                var am = new DatabaseManagement.AccountManagement();

                return am.Exists(testAccount.User);
            }
            public static void Register(SpaceShip ship, Func<(string accountName, string password)> registerScreen)
            {
                //testAccount add ship
                var userinput = registerScreen();
                //testAccount 
                var userName = userinput.accountName;
                var password = userinput.password;
                var am = new DatabaseManagement.AccountManagement();
                am.Register(testAccount.User, userName, password); //add spaceship
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