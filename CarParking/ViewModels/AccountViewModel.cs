using CarParking.Data;
using CarParking.Models;
using CarParking.Windows;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarParking.ViewModels
{
    class AccountViewModel : BindableBase
    {
        public ObservableCollection<Account> Accounts { get; set; } = new();

        private readonly AppDbContext _AppDbContext;

        public AccountViewModel(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;

            Accounts = new ObservableCollection<Account>(_AppDbContext.Accounts.ToList());
        }

        public ICommand RemoveItem => new DelegateCommand<Account>(async (Account) =>
        {
            var OrderToDelate = _AppDbContext.Accounts.Find(Account.Id);

            _AppDbContext.Accounts.Remove(OrderToDelate);

            await _AppDbContext.SaveChangesAsync();

            Accounts = new ObservableCollection<Account>(await _AppDbContext.Accounts.ToListAsync());

        }, (Account) => Account != null);

        public ICommand AddAccount => new DelegateCommand(async () =>
        {
            new AddAcccountWindow().Show();
        });

        public ICommand UpdAccount => new DelegateCommand(async () =>
        {
            Accounts = new ObservableCollection<Account>(await _AppDbContext.Accounts.ToListAsync());
        });
    }
}
