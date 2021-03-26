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
        static void Main(string[] args)
        {

            ShowWindow(ThisConsole, 3);
            Console.CursorVisible = false;

            var option = Option.Welcome;

            while(true)
            {
                switch (option)
                {
                    case Option.Welcome:
                        option = Screen.Welcome();
                        Thread.Sleep(1000);
                        break;
                    case Option.Start:
                        option = Screen.Start();
                        break;
                    case Option.Identification:
                        option = Screen.Identification();
                        break;
                    case Option.Login:
                        option = Screen.Login();
                        break;
                    case Option.Registration:
                        option = Screen.Registration();
                        break;
                    case Option.RegisterShip:
                        option = Screen.RegisterShip();
                        break;
                    case Option.Account:
                        option = Screen.Account();
                        break;
                    case Option.Parking:
                        option = Screen.Parking();
                        break;
                    case Option.Homeplanet:
                        option = Screen.HomePlanet();
                        break;
                    case Option.Receipts:
                        option = Screen.Receipts();
                        break;
                    case Option.Exit:
                        Screen.Exit();
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    case Option.ReRegisterShip:
                        option = Screen.RegisterShip(reRegister: true);
                        break;
                    case Option.Logout:
                        _account = null;
                        option = Screen.Start();
                        break;
                    default:
                        throw new Exception("Something wrong with the options");
                        break;
                }
            }
            
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}