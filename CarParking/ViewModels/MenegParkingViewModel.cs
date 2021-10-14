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
    class MenegParkingViewModel : BindableBase
    {
        private readonly AppDbContext _AppDbContext;

        private readonly EventBus _EventBus;

        private readonly СurrentUserService _CurrentUserService;

        public ObservableCollection<ParkingPlace> ParkingPlaces { get; set; } = new();

        public ObservableCollection<Car> Cars { get; set; } = new();

        public Car SelectedCar { get; set; }

        public bool IsAdmin { get; set; }

        public MenegParkingViewModel(AppDbContext appDbContext, EventBus eventBus, СurrentUserService сurrentUserService)
        {
            _AppDbContext = appDbContext;

            _EventBus = eventBus;

            _CurrentUserService = сurrentUserService;

            ParkingPlaces = new ObservableCollection<ParkingPlace>(_AppDbContext.ParkingPlaces.ToList());

            _EventBus.Subscribe<AdminAut>(async @event =>
            {
                IsAdmin = true;

                Cars = new ObservableCollection<Car>(await _AppDbContext.Cars.ToListAsync());
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

        public ICommand UpdateList => new DelegateCommand(async () =>
        {
            ParkingPlaces = new ObservableCollection<ParkingPlace>(await _AppDbContext.ParkingPlaces.ToListAsync());

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

        public ICommand TakePlace => new DelegateCommand<ParkingPlace>(async (ParkingPlace) =>
        {
            if (SelectedCar == null) return;

            ParkingPlace.IsEnable = false;

            ParkingPlace.Account = _AppDbContext.Accounts.Find(_CurrentUserService.GetСurrentAccount().Id);

            ParkingPlace.Car = SelectedCar;

            _AppDbContext.Entry(ParkingPlace).State = EntityState.Modified;

            var historyResult = new ParkingHistory() {DateTimeEvent = DateTime.Now, Car = SelectedCar, Account = ParkingPlace.Account, ParkingPlace = ParkingPlace, IsEnable = false};

            _AppDbContext.ParkingHistories.Add(historyResult);

            _AppDbContext.SaveChanges();

            Cars = new ObservableCollection<Car>(await _AppDbContext.Cars.ToListAsync());

        }, (ParkingPlace) => ParkingPlace != null);

    }
}
