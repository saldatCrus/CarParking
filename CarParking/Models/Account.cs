using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParking.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Login { get; set; }

        public List<Car> Cars { get; set; }

        public bool IsAdministrator { get; set; }
    }
}
