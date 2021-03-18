using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int StarWarsID { get; set; }
        public string Gender { get; set; }
        public string Eye_color { get; set; }
        public User(string name, int id)
        {
            Name = name;
            StarWarsID = id;
            
        }

        public User()
        {
            
        }
       
    }
}
