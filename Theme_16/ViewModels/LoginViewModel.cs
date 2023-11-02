﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.ModelViews.Base;
using Theme_16.Services;
using Theme_16.Stores;

namespace Theme_16.ViewModels
{
    internal class LoginViewModel : ViewModel
    {
        private readonly string _login = "User123";
        private readonly string _password = "password123";
        private readonly NavigationService<NavigationStore, MainViewModel> _mainVM_NavigationService;
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
            _mainVM_NavigationService.Navigate();


            //if (CheckedPassword == _password && CheckedLogin == _login)
            //{               
                
            //}
            //else
            //{
            //    MessageBox.Show("Неправильный логин и/или пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }


        public LoginViewModel(NavigationService<NavigationStore, MainViewModel> navigationService)
        {
            _mainVM_NavigationService = navigationService;
        }

        public LoginViewModel()
        {
        }
    }
}