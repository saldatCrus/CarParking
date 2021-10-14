using CarParking.Data;
using CarParking.Events;
using CarParking.Models;
using CarParking.Service;
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
    class CarViewModel : BindableBase
    {
        public ObservableCollection<Account> Accounts { get; set; } = new();

        public ObservableCollection<Car> Cars { get; set; } = new();

        private readonly AppDbContext _AppDbContext;

        private readonly EventBus _EventBus;

        private readonly СurrentUserService _CurrentUserService;

        public Account User { get; set; }

        private bool IsAdmin { get; set; }

        public CarViewModel(AppDbContext appDbContext, EventBus eventBus, СurrentUserService сurrentUserService)
        {
            _AppDbContext = appDbContext;

            _EventBus = eventBus;

            _CurrentUserService = сurrentUserService;


            _EventBus.Subscribe<AdminAut>(async @event =>
            {
                Cars = new ObservableCollection<Car>(await _AppDbContext.Cars.ToListAsync());

                IsAdmin = true;
            });

            _EventBus.Subscribe<UserAut>(async @event =>
            {
                var resultAccount = await _AppDbContext.Accounts.FindAsync(_CurrentUserService.GetСurrentAccount().Id);

                var result = _AppDbContext.Accounts.Include(c => c.Cars).ToList();

                if (resultAccount.Cars == null) resultAccount.Cars = new();

                Cars = new ObservableCollection<Car>(resultAccount.Cars);

                IsAdmin = false;
            });
        }

        public ICommand AddCar => new DelegateCommand(() =>
        {
            new AddCarWindow().Show();
        });

        public ICommand UpdateList => new DelegateCommand(async() =>
        {
            if (IsAdmin)
            {
                Cars = new ObservableCollection<Car>(await _AppDbContext.Cars.ToListAsync());
            }
            else
            {
                var resultAccount = await _AppDbContext.Accounts.FindAsync(_CurrentUserService.GetСurrentAccount().Id);

                var result = _AppDbContext.Accounts.Include(c => c.Cars).ToList();

                if (resultAccount.Cars == null) resultAccount.Cars = new();

                Cars = new ObservableCollection<Car>(resultAccount.Cars);
            } 

        });

        public ICommand RemoveItem => new DelegateCommand<Car>(async (Car) =>
        {
            var OrderToDelate = _AppDbContext.Cars.Find(Car.Id);

            _AppDbContext.Cars.Remove(OrderToDelate);

            _AppDbContext.SaveChanges();

            Cars = new ObservableCollection<Car>(await _AppDbContext.Cars.ToListAsync());

        }, (Car) => Car != null);
    }
}
