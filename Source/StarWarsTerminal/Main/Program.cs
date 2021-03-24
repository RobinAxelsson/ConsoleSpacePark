using System;
using StarWarsApi.Models;
using StarWarsTerminal.UI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using StarWarsApi.Networking;
using Newtonsoft.Json;
using StarWarsApi.Database;

namespace StarWarsTerminal.Main
{
    static partial class Program
    {

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
        public enum StartMenuOptions
        {
            Login,
            NewAccount,
            Exit
        }
        public enum LoginMenuOptions
        {
            Park,
            CheckReceipts,
            ReRegisterShip,
            GoToHomeplanet,
            Exit
        }
        public enum ParkingMenuOptions
        {
            PurchaseTicket,
            ReEnterhours,
            BackToLogin

        }
        static void Main(string[] args)
        {
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.CursorVisible = false;
            Thread.Sleep(500);
            //IdentificationScreen();
            //RegistrationScreen();
            //WelcomeScreen();
            //Console.ReadLine();
            //ConsoleWriter.ClearScreen();
            //ChooseShipScreen(GetLocalShips());
            //StartFlow();
            //AccountScreen();
            //ShipScreen();
            Console.ReadLine();
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
        public static void AccountFlow()
        {
            var option = AccountScreen();
            ConsoleWriter.ClearScreen();

            switch (option)
            {
                case LoginMenuOptions.Park:
                    break;
                case LoginMenuOptions.CheckReceipts:
                    break;
                case LoginMenuOptions.ReRegisterShip:
                    break;
                case LoginMenuOptions.GoToHomeplanet:
                    break;
                case LoginMenuOptions.Exit:
                    ExitScreen();
                    break;
                default:
                    break;
            }
        }
        public static void ParkingFlow()
        {
            var option = ParkingScreen();
            ConsoleWriter.ClearScreen();
            switch (option)
            {
                case ParkingMenuOptions.PurchaseTicket:
                    break;
                case ParkingMenuOptions.ReEnterhours:
                    break;
                case ParkingMenuOptions.BackToLogin:
                    AccountFlow();
                    break;
                default:
                    break;
            }
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