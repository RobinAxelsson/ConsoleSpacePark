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
    static partial class Program
    {
        static Program()
        {
            DatabaseManagement.ConnectionString = @"Server = 90.229.161.68,52578; Database = StarWarsProject2.3; User Id = adminuser; Password = starwars;";
        }
        public enum Option
        {
            StartScreen,
            Login,
            Park,
            CheckReceipts,
            RegisterLogin,
            ReRegisterShip,
            RegisterShip,
            GoToHomeplanet,
            NewAccount,
            GotoAccount,
            ReEnterhours,
            PurchaseTicket,
            Exit
        }
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        public const ConsoleColor ForegroundColor = ConsoleColor.Green;

        public static Account _account { get; set; } = new Account();
        public static SpaceShip _ship { get; set; } = new SpaceShip();
        public static (string accountName, string password) _namepass { get; set; }
        static void Main(string[] args)
        {
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.CursorVisible = false;
            Screen.Welcome();
            Console.ReadLine();
            var option = Option.StartScreen;

            while(option != Option.Exit)
            {
                switch (option)
                {
                    case Option.StartScreen:
                        option = Screen.StartScreen();
                        break;
                    case Option.Login:
                        option = Screen.LoginPasswordScreen();
                        break;
                    case Option.GotoAccount:
                        option = Screen.AccountScreen();
                        break;
                    case Option.Park:
                        option = Screen.ParkingScreen();
                        break;
                    case Option.CheckReceipts:
                        break;
                    case Option.RegisterShip:
                        option = Screen.ShipScreen();
                        break;
                    case Option.RegisterLogin:
                        option = Screen.RegistrationScreen();
                        break;
                    case Option.ReRegisterShip:
                        option = Screen.ShipScreen();
                        break;
                    case Option.GoToHomeplanet:
                        option = Screen.HomePlanetScreen();
                        break;
                    case Option.NewAccount:
                        option = Screen.IdentificationScreen();
                        break;
                    case Option.PurchaseTicket:
                        break;
                    default:
                        break;
                }
            }
            Screen.ExitScreen();
            Thread.Sleep(2000);
        }
        public enum StartMenuOptions
        {
            Login,
            NewAccount,
            Exit
        }
        public static void StartFlow()
        {
            var startOption = StartScreen();
            ConsoleWriter.ClearScreen();

            switch (startOption)
            {
                case StartMenuOptions.Login:
                    var accountPass = LoginPasswordScreen();
                    break;
                case StartMenuOptions.NewAccount:
                    IdentificationScreen();
                    break;
                case StartMenuOptions.Exit:
                    ExitScreen();
                    break;
                default:
                    break;
            }
        }
        public enum AccountMenuOptions
        {
            Park,
            CheckReceipts,
            ReRegisterShip,
            GoToHomeplanet,
            Exit
        }
        public static void AccountFlow()
        {
            var option = AccountScreen();
            ConsoleWriter.ClearScreen();

            switch (option)
            {
                case AccountMenuOptions.Park:
                    ParkingScreen();
                    break;
                case AccountMenuOptions.CheckReceipts:
                    break;
                case AccountMenuOptions.ReRegisterShip:
                    var newShip = ShipScreen();
                    if (newShip != null)
                        _account.SpaceShip = newShip;
                    AccountFlow();
                    break;
                case AccountMenuOptions.GoToHomeplanet:
                    HomePlanetScreen();
                    break;
                case AccountMenuOptions.Exit:
                    ExitScreen();
                    break;
                default:
                    break;
            }
        }
        public enum ParkingMenuOptions
        {
            PurchaseTicket,
            ReEnterhours,
            BackToLogin
        }

        public static SpaceShip[] GetLocalShips()
        {
            string jsonstring = File.ReadAllText(@"UI/json/small-ships.json");
            return JsonConvert.DeserializeObject<SpaceShip[]>(jsonstring);
        }
        public static (string FullName, string Password) GetNamePass(List<IDrawable> drawables)
        {
            var colons = drawables.FindAll(x => x.Chars == ":");
            var nameLine = new InputLine(colons[0], 50, ForegroundColor);

            string fullname = nameLine.GetInputString(false);
            var passwordLine = new InputLine(colons[1], 50, ForegroundColor);
            string password = passwordLine.GetInputString(isPassword: true);

            LineTools.ClearAt((nameLine.X, nameLine.Y), fullname);
            LineTools.ClearAt((passwordLine.X, passwordLine.Y), password);
            
            return (fullname, password);
        }
    }
}