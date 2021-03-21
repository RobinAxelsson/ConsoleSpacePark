using System;
using StarWarsApi.Models;
using StarWarsApi.Repository;
using StarWarsApi.Database;
using StarWarsTerminal.UI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.IO;

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

            var list = new string[]
            {
                "bobba",
                "Yabba",
                "Yoda",
                "Tatoine",
                "Jedi"
            };
            string[] lines = File.ReadAllLines(@"UI/TextFrames/list-container.txt");
            var listText = new TextContainer(lines, ForegroundColor);
            listText.AddLines(list, 3);
            listText.CenterUnitsXDir(Console.WindowWidth);
            listText.CenterUnitsYDir(Console.WindowHeight);
            ConsoleWriter.TryAppend(listText);
            ConsoleWriter.Update();

            Console.ReadLine();
        }

        public static (string FullName, string Password) GetNamePass(TextContainer credScreen)
        {
            var fullNameInput = new InputLine(credScreen.Drawables.Find(x => x.Chars == ":"), 50, ForegroundColor);
            string fullname = fullNameInput.GetInputString(false);
            var passwordInput = new InputLine(credScreen.Drawables.FindLast(x => x.Chars == ":"), 50, ForegroundColor);
            string password = passwordInput.GetInputString(isPassword: true);
            return (fullname, password);
        }
        public static TextContainer SetupWelcomeScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/welcome-screen.txt");
            var welcomeText = new TextContainer(lines, ForegroundColor);
            welcomeText.CenterUnitsXDir(Console.WindowWidth);
            welcomeText.CenterUnitsYDir(Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();
            return welcomeText;
        }
        public static TextContainer SetupCredentialsScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/enter-credentials.txt");
            var credentialText = new TextContainer(lines, ForegroundColor);
            credentialText.CenterUnitsXDir(Console.WindowWidth);
            credentialText.CenterUnitsYDir((int)Math.Round(Console.WindowHeight * 0.8));
            ConsoleWriter.TryAppend(credentialText);
            ConsoleWriter.Update();
            return credentialText;
        }
    }
}