namespace StarWarsApi.Models
{
    public class Hangar
    {
     public int HangarID { get; set; }
     public Account Account { get; set; }
     public string Name { get; set; }
     public long Price { get; set; }
    }
}