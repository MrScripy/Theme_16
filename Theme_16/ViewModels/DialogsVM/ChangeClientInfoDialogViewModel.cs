using System.Windows.Input;
using System.Windows;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.Models;
using Theme_16.ModelViews.Base;
using Theme_16.Services;
using Theme_16.Views.Dialogs;

namespace Theme_16.ViewModels.DialogsVM
{
    internal class ChangeClientInfoDialogViewModel : ViewModel
    {
        #region Properties

        private readonly TransferCustomerService _transferCustomerService;
        private Person _person;

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

        private int _phone;
        public int Phone
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


        public ChangeClientInfoDialogViewModel(TransferCustomerService transferCustomerService)
        {
            _transferCustomerService = transferCustomerService;
            _person = _transferCustomerService.Customer;

            _name = _person.Name;
            _patronymic = _person.Patronymic;
            _surname = _person.Surname;
            _phone = _person.Phone;
            _mail = _person.Mail;
        }

        private ICommand _changeCustomerCommand;
        public ICommand ChangeCustomerCommand => _changeCustomerCommand ??=
            new LambdaCommand(OnChangeCustomerCommandExecuted, CanChangeCustomerCommandExecute);

        private bool CanChangeCustomerCommandExecute(object p) => true;
        private void OnChangeCustomerCommandExecuted(object p)
        {
            
            _person.Name = Name;
            _person.Patronymic = Patronymic;
            _person.Surname = Surname;
            _person.Phone = Phone;

            App.CurrentWindow.Close();
        }


    }
}
