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
    class AddParkingViewModel : BindableBase
    {
        public string NameTxt { get; set; }

        private readonly AppDbContext _AppDbContext;

        public AddParkingViewModel(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public ICommand AddParckingPlace => new DelegateCommand(async () =>
        {
            var result = new ParkingPlace()
            {
                Name = NameTxt
            };

            _AppDbContext.ParkingPlaces.Add(result);

            await _AppDbContext.SaveChangesAsync();
        });
    }
}
