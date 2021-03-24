using Microsoft.EntityFrameworkCore;
using StarWarsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApi.Database
{
    public class StarWarsContext : DbContext
    {
        public string ConnectionString; //hard code connectionstring in onconfiguring method when migrating
        public DbSet<SpaceShip> SpaceShips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Hangar> Hangars { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<User.Homeworld> Homeworlds { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
           // ConnectionString = "";
            optionsbuilder.UseSqlServer(ConnectionString);
        }
    }
}
