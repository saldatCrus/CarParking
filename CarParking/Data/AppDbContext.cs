using CarParking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace CarParking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<ParkingPlace> ParkingPlaces { get; set; }

        public DbSet<ParkingHistory> ParkingHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=usersdata.db");
        }
    }
}
