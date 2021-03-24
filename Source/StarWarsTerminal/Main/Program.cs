using System;
using StarWarsApi.Models;
using StarWarsTerminal.UI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using StarWarsApi.Interfaces;
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
            IdentificationScreen();
            //RegistrationScreen();
            //WelcomeScreen();
            //Console.ReadLine();
            //ConsoleWriter.ClearScreen();
            //StartFlow();
            Console.ReadLine();
        }
        public static void StartFlow()
        {
            var startOption = StartMenu();
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
                    Exit();
                    break;
                default:
                    break;
            }
        }
        public static void LoginFlow()
        {
            var option = LoginMenu();
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
                    Exit();
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
                    LoginFlow();
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
            var fullNameInput = new InputLine(colons[0], 50, ForegroundColor);
            string fullname = fullNameInput.GetInputString(false);
            var passwordInput = new InputLine(colons[1], 50, ForegroundColor);
            string password = passwordInput.GetInputString(isPassword: true);
            Console.SetCursorPosition(fullNameInput.X, fullNameInput.Y);
            foreach (char chr in fullname)
                Console.Write(" ");

            Console.SetCursorPosition(passwordInput.X, passwordInput.Y);
            foreach (char chr in password)
                Console.Write(" ");
            return (fullname, password);
        }
    }
}