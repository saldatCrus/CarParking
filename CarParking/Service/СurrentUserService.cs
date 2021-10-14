using CarParking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParking.Service
{
    public class СurrentUserService
    {
        private Account Account;

        public СurrentUserService()
        {

        }

        public Account GetСurrentAccount()
        {
            return Account;
        }

        public void SetСurrentAccount(Account account)
        {
            Account = account;
        }
    }
}
