using CarParking.Data;
using CarParking.Service;
using CarParking.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParking
{
    class ViewModelLocator
    {
        private static ServiceProvider _provaider;

        public static void Init()
        {
            var services = new ServiceCollection();

            //My ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<ParkingViewModel>();
            services.AddTransient<AutorisationViewModel>();
            services.AddTransient<AccountViewModel>();
            services.AddTransient<AddAcccountViewModel>();
            services.AddTransient<AddParkingViewModel>();
            services.AddSingleton<CarViewModel>();
            services.AddSingleton<AddCarViewModel>();
            services.AddTransient<MenegParkingViewModel>();



            // My service
            services.AddSingleton<EventBus>();
            services.AddSingleton<СurrentUserService>();
            services.AddSingleton<MessageBus>();
            services.AddTransient<AppDbContext>();

            _provaider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provaider.GetRequiredService(item.ServiceType);
            }
        }

        public MainViewModel MainViewModel => _provaider.GetRequiredService<MainViewModel>();

        public ParkingViewModel ParkingViewModel => _provaider.GetRequiredService<ParkingViewModel>();

        public AutorisationViewModel AutorisationViewModel => _provaider.GetRequiredService<AutorisationViewModel>();

        public AccountViewModel AccountViewModel => _provaider.GetRequiredService<AccountViewModel>();

        public AddAcccountViewModel AddAcccountViewModel => _provaider.GetRequiredService<AddAcccountViewModel>();

        public AddParkingViewModel AddParkingViewModel => _provaider.GetRequiredService<AddParkingViewModel>();

        public CarViewModel CarViewModel => _provaider.GetRequiredService<CarViewModel>();

        public AddCarViewModel AddCarViewModel => _provaider.GetRequiredService<AddCarViewModel>();

        public MenegParkingViewModel MenegParkingViewModel => _provaider.GetRequiredService<MenegParkingViewModel>();

    }
}
