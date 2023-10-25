using System.Windows;
using System.Windows.Input;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.ModelViews.Base;

namespace Theme_16.ViewModels
{
    internal class LoginViewModel : ViewModel
    {
        private readonly string _login = "User123";
        private readonly string _password = "password123";

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
            if(CheckedLogin.Length > 0 && CheckedPassword.Length > 0) return true;
            return false;
        }
        private void OnConnectCommandExecuted(object p)
        {
            if(CheckedPassword == _password && CheckedLogin == _login)
            {
                MessageBox.Show("Все правильно, но дальше я пока не придумала", "Гуд!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Неправильный логин и/или пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
