using StarWarsTerminal.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsTerminal.UI.Screen
{
    public static class Map
    {
        public static string GetFilePath(Option option)
        {
            string filepath = "";
            switch (option)
            {
                case Option.Welcome:
                    filepath = @"UI/maps/1.Welcome.txt";
                    break;
                case Option.Start:
                    filepath = @"UI/maps/2.Start.txt";
                    break;
                case Option.Identification:
                    filepath = @"UI/maps/3a.Identification.txt";
                    break;
                case Option.Login:
                    filepath = @"UI/maps/3b.Login.txt";
                    break;
                case Option.Registration:
                    filepath = @"UI/maps/4a.Registration.txt";
                    break;
                case Option.RegisterShip:
                    filepath = @"UI/maps/5a.RegisterShip.txt";
                    break;
                case Option.Account:
                    filepath = @"UI/maps/6.Account.txt";
                    break;
                case Option.Homeplanet:
                    filepath = @"UI/maps/7.Homeplanet.txt";
                    break;
                case Option.Parking:
                    filepath = @"UI/maps/7.Parking.txt";
                    break;
                case Option.Receipts:
                    filepath = @"UI/maps/7.Receipts.txt";
                    break;
                case Option.Exit:
                    filepath = @"UI/maps/7.Exit.txt";
                    break;
                default:
                    throw new Exception("Option does not exist as filepath.");
            }
            return filepath;
        }
    }
}
