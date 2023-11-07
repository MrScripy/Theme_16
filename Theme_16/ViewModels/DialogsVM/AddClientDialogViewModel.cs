using System.Windows.Input;
using System.Windows;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.ModelViews.Base;
using Theme_16.Views.Dialogs;
using Theme_16.Services;

namespace Theme_16.ViewModels.DialogsVM
{
    internal class AddClientDialogViewModel : ViewModel
    {
        #region Properties
        private readonly TransferCustomerService _transferService;


        private string _name = "Test";
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _patronymic = "Test";
        public string Patronymic
        {
            get => _patronymic;
            set => Set(ref _patronymic, value);
        }

        private string _surname = "Test";
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        private int _phone = 123;
        public int Phone
        {
            get => _phone;
            set => Set(ref _phone, Phone);
        }
        private string _mail = "Test";
        public string Mail
        {
            get => _mail;
            set => Set(ref _mail, value);
        } 
        #endregion

        public AddClientDialogViewModel(TransferCustomerService transferService)
        {
            _transferService = transferService;
        }

        private ICommand _addCustomerCommand;

        public ICommand AddCustomerCommand => _addCustomerCommand ??=
            new LambdaCommand(OnAddCustomerCommandExecuted, CanAddCustomerCommandExecute);

        private bool CanAddCustomerCommandExecute(object p)
        {
            return true;
        }
        private void OnAddCustomerCommandExecuted(object p)
        {
            _transferService.Customer = new Models.Person()
            {
                Name = Name, 
                Patronymic = Patronymic,
                Surname = Surname,
                Phone = Phone,
                Mail = Mail
            };
        }
    }
}
