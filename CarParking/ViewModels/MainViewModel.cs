using CarParking.Events;
using CarParking.Pages;
using CarParking.Service;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarParking.ViewModels
{
    class MainViewModel : BindableBase
    {
        public Page MemberTrackPage { get; set; }

        public bool MenegParkingEnable { get; set; }

        public bool ParkingEnable { get; set; }

        public bool AccountEnable { get; set; }

        public bool CarsEnable { get; set; }

        private readonly EventBus _EventBus;

        public MainViewModel(EventBus eventBus)
        {
            _EventBus = eventBus;

            MenegParkingEnable = false;

            ParkingEnable = false;

            AccountEnable = false;

            CarsEnable = false;


            _EventBus.Subscribe<AdminAut>(async @event =>
            {
                MenegParkingEnable = true;

                ParkingEnable = true;

                AccountEnable = true;

                CarsEnable = true;
            });

            _EventBus.Subscribe<UserAut>(async @event =>
            {
                MenegParkingEnable = true;

                ParkingEnable = false;

                AccountEnable = false;

                CarsEnable = true;

            });
        }

        public ICommand OpAutorisation => new DelegateCommand(() =>
        {
            MemberTrackPage = new AutorisationPage();
        });

        public ICommand OpAccount => new DelegateCommand(() =>
        {
            MemberTrackPage = new AccountPage();
        });

        public ICommand OpParking => new DelegateCommand(() =>
        {
            MemberTrackPage = new ParkingPage();
        });

        public ICommand OpCars => new DelegateCommand(() =>
        {
            MemberTrackPage = new CarPage();
        });

        public ICommand OpMenegParking => new DelegateCommand(() =>
        {
            MemberTrackPage = new MenegParkingPage();
        });


    }
}
