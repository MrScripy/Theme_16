using System.Windows.Input;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.ModelViews.Base;
using Theme_16.Services;
using System;

namespace Theme_16.ViewModels.DialogsVM
{
    internal class AddCustomerDialogViewModel : ViewModel
    {
        #region Properties
        private readonly TransferCustomerService _transferService;


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

        private string _phoneNum;
        public string PhoneNum
        {
            get => _phoneNum;
            set => Set(ref _phoneNum, value);
        }

        private string _mail;
        public string Mail
        {
            get => _mail;
            set => Set(ref _mail, value);
        }
        #endregion

        public AddCustomerDialogViewModel(TransferCustomerService transferService)
        {
            _transferService = transferService;
        }

        private ICommand _addCustomerCommand;

        public ICommand AddCustomerCommand => _addCustomerCommand ??=
            new LambdaCommand(OnAddCustomerCommandExecuted, CanAddCustomerCommandExecute);

        private bool CanAddCustomerCommandExecute(object p)
        {
            if (!string.IsNullOrEmpty(Mail) && int.TryParse(PhoneNum, out _)) return true;
            return false;
        }
        private void OnAddCustomerCommandExecuted(object p)
        {
            if (!string.IsNullOrEmpty(Mail))
            {
                _transferService.Customer = null;
            }
            else
            {
                _transferService.Customer = new Models.Customer()
                {
                    Name = Name,
                    Patronymic = Patronymic,
                    Surname = Surname,
                    Phone = Int32.Parse(PhoneNum),
                    Mail = Mail
                };
            }

            App.CurrentWindow.Close();
        }
    }
}
