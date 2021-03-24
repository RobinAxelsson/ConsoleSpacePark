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
        public static void WelcomeScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/1.welcome-screen.txt");
            var welcomeText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(welcomeText, Console.WindowWidth);
            TextEditor.Center.InYDir(welcomeText, Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();
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
        public static void IdentificationScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/3a.Identification.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Green;

            Func<string, (int X, int Y)> GetCoord = (chr) =>
            {
                var drawable = drawables.Find(x => x.Chars == chr);
                return (drawable.CoordinateX, drawable.CoordinateY);
            };
            Action<(int X, int Y)> ResetCursor = (coord) =>
            {
                Console.SetCursorPosition(coord.X, coord.Y);
            };
            Action<string> Clear = (clear) =>
            {
                Console.CursorVisible = false;
                foreach (char c in clear) Console.Write(" ");
                Console.CursorVisible = true;
            };

            var fCoord = GetCoord("F");
            var nameEndCoord = GetCoord(":");

            ResetCursor((nameEndCoord.X + 2, nameEndCoord.Y));
            var username = Console.ReadLine();

            ResetCursor(fCoord);
            Clear("First name: " + username);
            ResetCursor(fCoord);
            Console.Write("Security question loading...");

            Func<string, string> getSecurityAnswer = (securityQuestion) =>
            {
                ResetCursor(fCoord);
                Clear("Security question loading...");
                ResetCursor(fCoord);
                Console.Write(securityQuestion + " ");
                return Console.ReadLine();
            };

            var tryUser = DatabaseManagement.AccountManagement.IdentifyWithQuestion(username, getSecurityAnswer);

            ResetCursor(fCoord);
            Clear("Security question loading... plus the long answer that i cleared now!");
            ResetCursor(fCoord);

            if (tryUser == null)
            {

                Console.WriteLine("Wrong answer");
            }
            else
            {
                ConsoleWriter.ClearScreen();

                bool IsNewRegistration = DatabaseManagement.AccountManagement.TryRegistrate(tryUser, RegistrationScreen);
                if (IsNewRegistration == true)
                {
                    ConsoleWriter.ClearScreen();
                    var ship = ChooseShipScreen(GetLocalShips());
                    ConsoleWriter.ClearScreen();
                    //Takes user and ship

                }
                else
                {
                    ResetCursor(fCoord);
                    Console.WriteLine("User is already registered");
                    Thread.Sleep(500);
                    ConsoleWriter.ClearScreen();
                    LoginPasswordScreen();
                }
            }
        }
        public static (string AccountName, string Password) RegistrationScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/4a.register-account.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(drawables, Console.WindowWidth);
            TextEditor.Center.InYDir(drawables, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            Func<IDrawable, (int X, int Y)> getCoord = (drawable) => (drawable.CoordinateX + 2, drawable.CoordinateY);

            var colons = drawables.FindAll(x => x.Chars == ":");

            var accountStart = getCoord(colons[0]);
            var pass1 = getCoord(colons[1]);
            var pass2 = getCoord(colons[2]);

            Action<(int X, int Y)> ResetCursor = (coord) =>
            {
                Console.SetCursorPosition(coord.X, coord.Y);
            };

            Console.ForegroundColor = ConsoleColor.Green;
            ResetCursor(accountStart);

            var nameLine = new InputLine(accountStart.X, accountStart.Y, 30, ConsoleColor.Green);
            var pass1Line = new InputLine(pass1.X, pass1.Y, 30, ConsoleColor.Green);
            var pass2Line = new InputLine(pass2.X, pass2.Y, 30, ConsoleColor.Green);

            var accountName = "";
            var password1 = "";
            var password2 = "";
            bool mistake = false;

            do
            {
                if(mistake == true)
                {
                    ResetCursor((pass2.X, pass2.Y + 4));
                }
                accountName = nameLine.GetInputString(false);
                password1 = pass1Line.GetInputString(true);
                password2 = pass2Line.GetInputString(true);
                
                ResetCursor(accountStart);
                foreach (char c in accountName)
                    Console.Write(" ");

                ResetCursor(pass1);
                foreach (char c in password1)
                    Console.Write(" ");

                ResetCursor(pass2);
                foreach (char c in password2)
                    Console.Write(" ");

            } while (password1 != password2 || accountName.Length <= 5 || password1.Length <= 5);

            return (accountName, password2);
        }
        public static string DisplayQuestionGetAnswer(string securityQuestion)
        {
            Console.Write(securityQuestion + ": ");
            return Console.ReadLine();
        }
        public static (string AccountName, string Password) GetNewAccountNamePassword()
        {
            return ("BobaFett", "bfett123");
        }
        public static IStarwarsItem ChooseShipScreen(IStarwarsItem[] starwarsItems)
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