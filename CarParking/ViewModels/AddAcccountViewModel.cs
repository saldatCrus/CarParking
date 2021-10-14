using CarParking.Data;
using CarParking.Models;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarParking.ViewModels
{
    public class AddAcccountViewModel : BindableBase
    {
        public string FisrtNameTxt { get; set; }

        public string SecondNameTxt { get; set; }

        public string LastNameTxt { get; set; }

        public string LoginTxt { get; set; }

        public string PassTxt { get; set; }

        public bool IsAdministarorBool { get; set; }

        private readonly AppDbContext _AppDbContext;

        public AddAcccountViewModel(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public ICommand AddAccount => new DelegateCommand(async () =>
        {
            var result = new Account()
            {
                FirstName = FisrtNameTxt,
                SecondName = SecondNameTxt,
                LastName = LastNameTxt,
                Login = LoginTxt,
                Password = PassTxt,
                IsAdministrator = IsAdministarorBool
            };

            _AppDbContext.Accounts.Add(result);

            await _AppDbContext.SaveChangesAsync();
        });
    }
}
