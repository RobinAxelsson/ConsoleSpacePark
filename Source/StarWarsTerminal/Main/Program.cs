using System;
using StarWarsApi.Models;
using StarWarsApi.Networking;
using StarWarsApi.Database;
using StarWarsTerminal.UI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using StarWarsApi.Interfaces;

namespace StarWarsTerminal.Main
{
    class Program
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
      
        static void Main(string[] args)
        {
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.CursorVisible = false;
            Thread.Sleep(500);

            SetupWelcomeScreen();
            Console.ReadLine();
            ConsoleWriter.ClearScreen();

            var accountPass = SetupLoginScreen();
            ConsoleWriter.ClearScreen();

            var spaceShips = new SpaceShip[]
            {
                new SpaceShip(){Name = "millenium falcon" },
                new SpaceShip(){Name = "star destroyer" },
                new SpaceShip(){Name = "Death star" },
                new SpaceShip(){Name = "X-wing" },
            };
            var output = CreateListPage(spaceShips);

            Console.ReadLine();
        }
        public static IStarwarsItem CreateListPage(IStarwarsItem[] starwarsItems)
        {
            var selector = new ListSelection(ForegroundColor, '$');
            var addLines = selector.ConvertIStarwarsItems(starwarsItems);

            string[] lines = File.ReadAllLines(@"UI/TextFrames/4a.choose-your-ship.txt");
            var listTitleDraws = TextEditor.DrawablesAt(lines, 0);
            TextEditor.AddWithSpacing(listTitleDraws, addLines, 2);
            TextEditor.CenterAllUnitsInXDir(listTitleDraws, Console.WindowWidth);
            TextEditor.CenterInYDir(listTitleDraws, Console.WindowHeight);
            selector.GetCharPositions(listTitleDraws);
            ConsoleWriter.TryAppend(listTitleDraws);
            ConsoleWriter.Update();
            return selector.GetSelection();
        }
        
        public static void SetupWelcomeScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/1.welcome-screen.txt");
            var welcomeText = TextEditor.DrawablesAt(lines, 0);
            TextEditor.CenterAllUnitsInXDir(welcomeText, Console.WindowWidth);
            TextEditor.CenterInYDir(welcomeText, Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();
        }
        public static (string FullName, string Password) SetupLoginScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/3b.login.txt");
            var loginText = TextEditor.DrawablesAt(lines, 0);
            TextEditor.CenterAllUnitsInXDir(loginText, Console.WindowWidth);
            TextEditor.CenterInYDir(loginText, Console.WindowHeight);
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
    }
}