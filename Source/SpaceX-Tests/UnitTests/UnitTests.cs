using StarWarsApi.Models;
using StarWarsApi.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static StarWarsApi.Database.DatabaseManagement;

namespace SpaceX_Tests.UnitTests
{
    public class DbTests
    {
        [Fact]
        public void ParseUserAsync_RightInput_ExpectBlue()
        {
            var user = APICollector.ParseUserAsync("Luke Skywalker");

            string expectedEyeColor = "blue";

            Assert.True(expectedEyeColor == user.EyeColor);
            //Assert.Equal(expectedEyeColor, user.eyeColor);
        }

        [Fact]
        public void ParseShipAsync_RightInput_ExpectCorellia()
        {
            var ship = APICollector.ParseShipAsync("YT-1300 light freighter");

            string expectedManufacturer = "Corellian Engineering Corporation";

            Assert.True(expectedManufacturer == ship.Manufacturer);
        }

        [Fact]
        public void CalculatePrice_RightInput_ExpectPricePerMinuteForCorvette()
        {
            var ship = APICollector.ParseShipAsync("CR90 corvette");
            
            decimal price = ParkingManagement.CalculatePrice(ship, 1);

            decimal expectedPrice = 15;

            Assert.Equal(expectedPrice, price);
        }

        [Fact]
        public void DoesUserExists_RightInput_ExpectLukeSkywalker()
        {
            ConnectionString = @"Server = 90.229.161.68,52578; Database = StarWarsProject2.3; User Id = adminuser; Password = starwars;";

            var user = APICollector.ParseUserAsync("Darth Maul");

            var expectedResult = AccountManagement.Exists(user.Name, false);

            Assert.True(expectedResult);
        }
    }
}
