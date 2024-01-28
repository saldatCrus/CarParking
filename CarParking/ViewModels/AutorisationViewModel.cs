using CarParking.Data;
using CarParking.Events;
using CarParking.Models;
using CarParking.Service;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarParking.ViewModels
{
    class AutorisationViewModel : BindableBase
    {
        public ObservableCollection<Account> Accounts { get; set; } = new();

        public string LoginTxt { get; set; }

        public string PassTxt { get; set; }

        private readonly AppDbContext _AppDbContext;

        private readonly EventBus _EventBus;

        private readonly СurrentUserService _CurrentUserService;

        public AutorisationViewModel(AppDbContext appDbContext, EventBus eventBus, СurrentUserService сurrentUserService)
        {
            _AppDbContext = appDbContext;

            _EventBus = eventBus;

            _CurrentUserService = сurrentUserService;

            Accounts = new ObservableCollection<Account>(_AppDbContext.Accounts.ToList());
        }

        public ICommand Aut => new DelegateCommand(async () =>
        {

            await _EventBus.Publish(new AdminAut());

            //foreach (var item in Accounts)
            //{
            //    if (item.Login == LoginTxt && item.Password == PassTxt)
            //    {
            //        if (item.IsAdministrator)
            //        {
            //            _CurrentUserService.SetСurrentAccount(item);
            //            await _EventBus.Publish(new AdminAut());

            //        }
            //        else
            //        {
            //            _CurrentUserService.SetСurrentAccount(item);
            //            await _EventBus.Publish(new UserAut());

            //        }

            //    }
            //}
        });
    }
}
