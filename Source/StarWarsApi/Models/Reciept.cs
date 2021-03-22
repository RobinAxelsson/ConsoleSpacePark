namespace StarWarsApi.Models
{
    public class Reciept
    {
        public int RecieptID { get; set; }
        public Account Account { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public long Price { get; set; }
        
    }
}