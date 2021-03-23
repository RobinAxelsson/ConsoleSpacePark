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
        static void Main(string[] args)
        {
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.CursorVisible = false;
            Thread.Sleep(500);

            //WelcomeScreen();
            //Console.ReadLine();
            //ConsoleWriter.ClearScreen();

            var startOption = StartMenu();

            switch (startOption)
            {
                case StartMenuOptions.Login:
                    ConsoleWriter.ClearScreen();
                    var accountPass = SetupLoginScreen();
                    break;
                case StartMenuOptions.NewAccount:
                    ConsoleWriter.ClearScreen();

                    break;
                case StartMenuOptions.Exit:
                    ConsoleWriter.ClearScreen();
                    Exit();
                    break;
                default:
                    break;
            }

            //var loginOption = LoginMenu();

            //switch (loginOption)
            //{
            //    case LoginMenuOptions.Park:
            //        break;
            //    case LoginMenuOptions.CheckReceipts:
            //        break;
            //    case LoginMenuOptions.ReRegisterShip:
            //        break;
            //    case LoginMenuOptions.GoToHomeplanet:
            //        break;
            //    case LoginMenuOptions.Exit:
            //        ConsoleWriter.ClearScreen();
            //        Exit();
            //        break;
            //    default:
            //        break;
            //}
        }
        //menu:
        //register
        //login
        //exit

        //input name
        //security question

        //Registration

        //Choose ship
        //var ships = GetLocalShips();
        //var output = CreateListPage(ships);

        //login


        //login-page

        //parking

        //home planet
        //view info
        //back to main

        //check receipts
        //view info
        //back to main

        //re-register ship
        //choose-ship

        //exit

        //Exit


        public static SpaceShip[] GetLocalShips()
        {
            string jsonstring = File.ReadAllText(@"UI/json/small-ships.json");
            return JsonConvert.DeserializeObject<SpaceShip[]>(jsonstring);
        }
        public static void IdentificationScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/4a.register-account.txt");
            var welcomeText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(welcomeText, Console.WindowWidth);
            TextEditor.Center.InYDir(welcomeText, Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();
            Console.ReadLine();
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
        public static (string FullName, string Password) SetupLoginScreen()
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