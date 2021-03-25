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
            RegistrationScreen,
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
                        //TODO: screen needed here? or just text?
                        break;
                    case Option.RegisterShip:
                        option = Screen.RegisterShip();
                        break;
                    case Option.RegistrationScreen:
                        option = Screen.RegistrationScreen();
                        break;
                    case Option.ReRegisterShip:
                        option = Screen.RegisterShip();
                        break;
                    case Option.GoToHomeplanet:
                        option = Screen.HomePlanetScreen();
                        break;
                    case Option.NewAccount:
                        option = Screen.IdentificationScreen();
                        break;
                    default:
                        break;
                }
            }
            Screen.ExitScreen();
            Thread.Sleep(2000);
        }
    }
}