using CarParking.Data;
using CarParking.Models;
using CarParking.Windows;
using DevExpress.Mvvm;
using ExcelLibrary.SpreadSheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarParking.ViewModels
{
    class ParkingViewModel : BindableBase
    {
        public ObservableCollection<ParkingPlace> ParkingPlaces { get; set; } = new();

        private readonly AppDbContext _AppDbContext;

        public ParkingViewModel(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;

            ParkingPlaces = new ObservableCollection<ParkingPlace>(_AppDbContext.ParkingPlaces.ToList());

            var accountResult = _AppDbContext.ParkingPlaces.Include(c => c.Account).ToList();

            var carResult = _AppDbContext.ParkingPlaces.Include(c => c.Car).ToList();
        }

        public ICommand AddParking => new DelegateCommand(async () =>
        {
            new AddParkingWindow().Show();
        });

        public ICommand UpdateList => new DelegateCommand(async () =>
        {
            var accountResult = _AppDbContext.ParkingPlaces.Include(c => c.Account).ToList();

            var carResult = _AppDbContext.ParkingPlaces.Include(c => c.Car).ToList();

            ParkingPlaces = new ObservableCollection<ParkingPlace>(await _AppDbContext.ParkingPlaces.ToListAsync());
        });

        public ICommand MakeReport => new DelegateCommand(async () =>
        {
            var result = await _AppDbContext.ParkingHistories.ToListAsync();

            var accountResult = _AppDbContext.ParkingHistories.Include(c => c.Account).ToList();

            var carResult = _AppDbContext.ParkingHistories.Include(c => c.Car).ToList();

            if (result == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.ShowDialog();

            var workbook = new Workbook();

            var worksheet = new Worksheet("Отчёт по стаянкам");

            worksheet.Cells[0, 0] = new Cell("Айди истоиий");
            worksheet.Cells[0, 1] = new Cell("Дата");
            worksheet.Cells[0, 2] = new Cell("Имя аккаунта");
            worksheet.Cells[0, 2] = new Cell("Фамилия аккаунта");
            worksheet.Cells[0, 3] = new Cell("Отчетсво аккаунта");
            worksheet.Cells[0, 4] = new Cell("Место парковки");
            worksheet.Cells[0, 5] = new Cell("Место свободно");

            for (int i = 0; i <= result.Count - 1; i++)
            {
                worksheet.Cells[i + 1, 0] = new Cell(result[i].Id.ToString());
                worksheet.Cells[i + 1, 1] = new Cell(result[i].DateTimeEvent.ToString());

                if (result[i].Account == null) 
                {
                    worksheet.Cells[i + 1, 2] = new Cell("аккаунт удалён");
                    worksheet.Cells[i + 1, 3] = new Cell("аккаунт удалён");
                    worksheet.Cells[i + 1, 4] = new Cell("аккаунт удалён");
                }
                else 
                {
                    worksheet.Cells[i + 1, 2] = new Cell(result[i].Account.FirstName);
                    worksheet.Cells[i + 1, 3] = new Cell(result[i].Account.SecondName);
                    worksheet.Cells[i + 1, 4] = new Cell(result[i].Account.LastName);
                }
                
                worksheet.Cells[i + 1, 5] = new Cell(result[i].IsEnable.ToString());
            }

            workbook.Worksheets.Add(worksheet);

            workbook.Save(saveFileDialog.FileName + ".xls");

        });

        public ICommand RemoveItem => new DelegateCommand<ParkingPlace>(async (ParkingPlace) =>
        {
            var OrderToDelate = _AppDbContext.ParkingPlaces.Find(ParkingPlace.Id);

            _AppDbContext.ParkingPlaces.Remove(OrderToDelate);

            await _AppDbContext.SaveChangesAsync();

            ParkingPlaces = new ObservableCollection<ParkingPlace>(await _AppDbContext.ParkingPlaces.ToListAsync());

        }, (ParkingPlace) => ParkingPlace != null);

        public ICommand FreePlace => new DelegateCommand<ParkingPlace>(async (ParkingPlace) =>
        {
            ParkingPlaces = new ObservableCollection<ParkingPlace>(await _AppDbContext.ParkingPlaces.ToListAsync());

            ParkingPlace.IsEnable = true;

            var historyResult = new ParkingHistory() { DateTimeEvent = DateTime.Now, Car = ParkingPlace.Car, Account = ParkingPlace.Account, ParkingPlace = ParkingPlace, IsEnable = true };

            _AppDbContext.ParkingHistories.Add(historyResult);

            ParkingPlace.Account = null;

            ParkingPlace.Car = null;

            _AppDbContext.Entry(ParkingPlace).State = EntityState.Modified;

            _AppDbContext.SaveChanges();

        }, (ParkingPlace) => ParkingPlace != null);
    }
}
