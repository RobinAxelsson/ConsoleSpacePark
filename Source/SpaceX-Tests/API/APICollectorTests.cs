using StarWarsApi.Networking;
using Xunit;
using static StarWarsApi.Database.DatabaseManagement;

namespace SpaceX_Tests.UnitTests
{
    public class APICollectorTests
    {
        [Fact]
        public void ParseUserAsync_RightInput_ExpectBlue()
        {
            var user = APICollector.ParseUserAsync("Luke Skywalker");

            var expectedEyeColor = "blue";

            Assert.True(expectedEyeColor == user.EyeColor);
            //Assert.Equal(expectedEyeColor, user.eyeColor);
        }

        [Fact]
        public void ParseShipAsync_RightInput_ExpectCorellia()
        {
            var ship = APICollector.ParseShipAsync("YT-1300 light freighter");

            var expectedManufacturer = "Corellian Engineering Corporation";

            Assert.True(expectedManufacturer == ship.Manufacturer);
        }

        [Fact]
        public void CalculatePrice_RightInput_ExpectPricePerMinuteForCorvette()
        {
            ConnectionString = "Insert connection string here.";
            var ship = APICollector.ParseShipAsync("CR90 corvette");
            var price = ParkingManagement.CalculatePrice(ship, 1);
            decimal expectedPrice = 15;
            Assert.Equal(expectedPrice, price);
        }

        [Fact]
        public void DoesUserExists_RightInput_ExpectDarthMaul()
        {
            ConnectionString = "Insert connection string here.";
            var user = APICollector.ParseUserAsync("Darth Maul");
            var expectedResult = AccountManagement.Exists(user);
            Assert.True(expectedResult);
        }
    }
}