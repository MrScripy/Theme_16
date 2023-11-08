using System.Windows;
using System.Windows.Input;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.ModelViews.Base;
using Theme_16.Services;
using Theme_16.Services.Interfaces;
using Theme_16.Stores;

namespace Theme_16.ViewModels
{
    internal class LoginViewModel : ViewModel
    {
        private readonly string _login = "user";
        private readonly string _password = "password123";
        private readonly NavigationService<NavigationStore, MainViewModel> _mainVM_NavigationService;
        private readonly IDataCreator _dataCreator;
        private string _checkedLogin = "";
        public string CheckedLogin
        {
            get => _checkedLogin;
            set => Set(ref _checkedLogin, value);
        }

        private string _checkedPassword = "";
        public string CheckedPassword
        {
            get => _checkedPassword;
            set => Set(ref _checkedPassword, value);
        }

        private ICommand _connectCommand;
        public ICommand ConnectCommand => _connectCommand ??=
            new LambdaCommand(OnConnectCommandExecuted, CanConnectCommandExecute);

        private bool CanConnectCommandExecute(object p)
        {
            if (CheckedLogin.Length > 0 && CheckedPassword.Length > 0) return true;
            return false;
        }
        private void OnConnectCommandExecuted(object p)
        {
            if (CheckedPassword == _password && CheckedLogin == _login)
            {
                _mainVM_NavigationService.Navigate();
            }
            else
            {
                MessageBox.Show("Wrong login and/or password", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public LoginViewModel(NavigationService<NavigationStore, MainViewModel> navigationService, IDataCreator dataCreator)
        {
            _mainVM_NavigationService = navigationService;
            _dataCreator = dataCreator;
            _dataCreator.FillDB();
        }

        public LoginViewModel() { }
    }
}
