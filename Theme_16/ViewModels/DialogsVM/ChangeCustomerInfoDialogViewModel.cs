using System.Windows.Input;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.Models;
using Theme_16.ModelViews.Base;
using Theme_16.Services;
using System;

namespace Theme_16.ViewModels.DialogsVM
{
    internal class ChangeCustomerInfoDialogViewModel : ViewModel
    {
        #region Properties

        private readonly TransferCustomerService _transferCustomerService;
        private Customer _customer;

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _patronymic;
        public string Patronymic
        {
            get => _patronymic;
            set => Set(ref _patronymic, value);
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, Phone);
        }
        private string _mail;
        public string Mail
        {
            get => _mail;
            set => Set(ref _mail, value);
        }

        #endregion


        public ChangeCustomerInfoDialogViewModel(TransferCustomerService transferCustomerService)
        {
            _transferCustomerService = transferCustomerService;
            _customer = _transferCustomerService.Customer;

            _name = _customer.Name;
            _patronymic = _customer.Patronymic;
            _surname = _customer.Surname;
            _phone = _customer.Phone.ToString();
            _mail = _customer.Mail;
        }

        private ICommand _changeCustomerCommand;
        public ICommand ChangeCustomerCommand => _changeCustomerCommand ??=
            new LambdaCommand(OnChangeCustomerCommandExecuted, CanChangeCustomerCommandExecute);

        private bool CanChangeCustomerCommandExecute(object p) => true;
        private void OnChangeCustomerCommandExecuted(object p)
        {
            
            _customer.Name = Name;
            _customer.Patronymic = Patronymic;
            _customer.Surname = Surname;
            _customer.Phone = Int32.Parse(Phone);

            App.CurrentWindow.Close();
        }


    }
}
