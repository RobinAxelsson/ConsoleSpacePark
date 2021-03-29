namespace StarWarsApi.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public User User { get; set; }
        public SpaceShip SpaceShip { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
    }
}