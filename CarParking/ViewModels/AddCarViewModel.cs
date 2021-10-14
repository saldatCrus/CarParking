using CarParking.Data;
using CarParking.Events;
using CarParking.Models;
using CarParking.Service;
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
    class AddCarViewModel : BindableBase
    {
        public ObservableCollection<Account> Accounts { get; set; } = new();

        public Account SelectedAccount { get; set; }

        public string NumberTxt { get; set; }

        public bool AccountsIsEnab { get; set; }

        private readonly AppDbContext _AppDbContext;

        private readonly EventBus _EventBus;

        private readonly СurrentUserService _CurrentUserService;

        public AddCarViewModel(AppDbContext appDbContext, EventBus eventBus, СurrentUserService сurrentUserService)
        {
            _AppDbContext = appDbContext;

            _EventBus = eventBus;

            _CurrentUserService = сurrentUserService;

            Accounts = new ObservableCollection<Account>(_AppDbContext.Accounts.ToList());

            _EventBus.Subscribe<AdminAut>(async @event =>
            {
                AccountsIsEnab = true;

                Accounts = new ObservableCollection<Account>(await _AppDbContext.Accounts.ToListAsync());
            });

            _EventBus.Subscribe<UserAut>(async @event =>
            {
                SelectedAccount = сurrentUserService.GetСurrentAccount();

                AccountsIsEnab = false;

            });

        }

        public ICommand AddCar => new DelegateCommand(async() =>
        {
            var resultcar = new Car() { Number = NumberTxt };

            var resultAccount = await _AppDbContext.Accounts.FindAsync(SelectedAccount.Id);

            if (resultAccount.Cars == null) resultAccount.Cars = new();

            resultAccount.Cars.Add(resultcar);

            _AppDbContext.Entry(resultAccount).State = EntityState.Modified;

            _AppDbContext.SaveChanges();
        });
    }
}
