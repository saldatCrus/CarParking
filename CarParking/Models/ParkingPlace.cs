using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParking.Models
{
    public class ParkingPlace
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Car Car { get; set; }

        public Account Account { get; set; }

        public bool IsEnable { get; set; }
    }
}
