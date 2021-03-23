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
    static class Program
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

            LoginFlow();

            WelcomeScreen();
            Console.ReadLine();
            ConsoleWriter.ClearScreen();

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
        public static ParkingMenuOptions ParkingScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/6.logged-in-menu.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<ParkingMenuOptions>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new ParkingMenuOptions[] { ParkingMenuOptions.PurchaseTicket, ParkingMenuOptions.ReEnterhours, ParkingMenuOptions.BackToLogin });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            return selectionList.GetSelection();
        }
        public static void IdentificationScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/3a.Identification.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            var drawable = drawables.Find(x => x.Chars == ":");
            Console.SetCursorPosition(drawable.CoordinateX + 2, drawable.CoordinateY);
            Console.ForegroundColor = ConsoleColor.Green;
            var username = Console.ReadLine();
            var inputUser = APICollector.ParseUserAsync(username);
            var security = DatabaseManagement.AccountManagement.GetSecurityQuestion(inputUser);
            Console.WriteLine(security.question);
            var inputAnswer = Console.ReadLine();
            if (inputAnswer.ToLower() == security.answer.ToLower())
            {
                Console.WriteLine("Correct!");
                DatabaseManagement.ConnectionString = @"Server=90.229.161.68,52578;Database=StarWarsProject2.1;User Id=adminuser;Password=starwars;";
                var am = new DatabaseManagement.AccountManagement();

                if (!am.Exists(inputUser))
                {
                    var userpass = LoginPasswordScreen();
                    
                    am.Register(inputUser, userpass.AccountName, userpass.Password);
                    Console.WriteLine("Successfully Registered!");
                    //Load register screen which contains only Account Name and Password
                    //Input userObject, string accountName, string password > Register method
                    //Then please login
                }
                else
                {
                    Console.WriteLine("This user is already registered!");
                }

            }
        }

        public static SpaceShip[] GetLocalShips()
        {
            string jsonstring = File.ReadAllText(@"UI/json/small-ships.json");
            return JsonConvert.DeserializeObject<SpaceShip[]>(jsonstring);
        }

        public static void RegistrationScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/4a.register-account.txt");
            var welcomeText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(welcomeText, Console.WindowWidth);
            TextEditor.Center.InYDir(welcomeText, Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();
            Console.ReadLine();
        }
        public static IStarwarsItem CreateListPage(IStarwarsItem[] starwarsItems)
        {
            var selector = new ListSelection(ForegroundColor, '$');
            var addLines = selector.ConvertIStarwarsItems(starwarsItems);

            string[] lines = File.ReadAllLines(@"UI/TextFrames/5a.choose-your-ship.txt");
            var listTitleDraws = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Add.DrawablesWithSpacing(listTitleDraws, addLines, 5);
            TextEditor.Center.AllUnitsInXDir(listTitleDraws, Console.WindowWidth);
            TextEditor.Center.InYDir(listTitleDraws, Console.WindowHeight);
            selector.GetCharPositions(listTitleDraws);
            ConsoleWriter.TryAppend(listTitleDraws);
            ConsoleWriter.Update();
            return selector.GetSelection();
        }

        public static void WelcomeScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/1.welcome-screen.txt");
            var welcomeText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(welcomeText, Console.WindowWidth);
            TextEditor.Center.InYDir(welcomeText, Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();
        }
        public static (string AccountName, string Password) LoginPasswordScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/3b.login.txt");
            var loginText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(loginText, Console.WindowWidth);
            TextEditor.Center.InYDir(loginText, Console.WindowHeight);
            ConsoleWriter.TryAppend(loginText);
            ConsoleWriter.Update();
            return GetNamePass(loginText);
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
        public static StartMenuOptions StartMenu()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/2.menu.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<StartMenuOptions>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new StartMenuOptions[] { StartMenuOptions.Login, StartMenuOptions.NewAccount, StartMenuOptions.Exit });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            return selectionList.GetSelection();
        }
        public static LoginMenuOptions LoginMenu()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/6.logged-in-menu.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<LoginMenuOptions>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new LoginMenuOptions[] { LoginMenuOptions.Park, LoginMenuOptions.CheckReceipts, LoginMenuOptions.ReRegisterShip, LoginMenuOptions.GoToHomeplanet, LoginMenuOptions.Exit });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            return selectionList.GetSelection();
        }
        public static void Exit()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/8.exit-screen.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}