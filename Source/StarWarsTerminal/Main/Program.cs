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
using System.Globalization;

namespace StarWarsTerminal.Main
{
    public static partial class Program
    {
        static Program()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            DatabaseManagement.ConnectionString = @"Server = 90.229.161.68,52578; Database = StarWarsProject2.6; User Id = adminuser; Password = starwars;";
        }
       
        public const ConsoleColor ForegroundColor = ConsoleColor.Green;
        public static Account _account { get; set; } = new Account();
        public static (string accountName, string password) _namepass { get; set; }
        static void Main3(string[] args)
        {

            ShowWindow(ThisConsole, 3);
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

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}