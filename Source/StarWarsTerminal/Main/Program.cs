using StarWarsApi.Database;
using StarWarsApi.Models;
using StarWarsTerminal.UI.Screens;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace StarWarsTerminal.Main
{
    public static class Program
    {
        public const ConsoleColor ForegroundColor = ConsoleColor.Green;
        private static readonly IntPtr ThisConsole = GetConsoleWindow();

        static Program()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            DatabaseManagement.SetConnectionString(@"Database/connection.txt"); //add connectionstring at this path and exact name to work with git ignore
        }

        public static Account _account { get; set; } = new();
        public static (string accountName, string password) _namepass { get; set; }

        private static void Main(string[] args)
        {
            ShowWindow(ThisConsole, 3);
            Console.CursorVisible = false;

            var option = Option.Welcome;

            while (true)
                switch (option)
                {
                    case Option.Welcome:
                        option = Screen.Welcome();
                        Console.ReadKey(true);
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
                        option = Screen.RegisterShip(true);
                        break;
                    case Option.Logout:
                        _account = null;
                        option = Screen.Start();
                        break;
                    default:
                        throw new Exception("Something wrong with the options");
                }
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}