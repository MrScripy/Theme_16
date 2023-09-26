using System.Collections.Generic;
using Theme_16.Models;
using Theme_16.ModelViews.Base;
using Theme_16.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Windows.Input;
using Theme_16.Infrastrucutre.Commands;
using System.Threading.Tasks;

namespace Theme_16.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly string _connectionCustomersString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Barbarossa\1_C#\16\Theme_16\Theme_16\Data\CustomersDB.mdf;Integrated Security=True";
        private readonly string _connectionOrdersString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Barbarossa\1_C#\16\Theme_16\Theme_16\Data\OrdersDB.mdf;Integrated Security=True";

        private IDataCreator _dataCreator;

        private IEnumerable<Person> _customers;
        public IEnumerable<Person> Customers
        {
            get => _customers;
            private set => Set(ref _customers, value);
        }

        IEnumerable<Order> _orders;
        public IEnumerable<Order> Orders
        {
            get => _orders;
            private set => Set(ref _orders, value); 
        }


        private ICommand _downloadDataCommand;
        public ICommand DownloadDataCommand => _downloadDataCommand ??=
            new LambdaCommand(OnDownloadDataCommandExecuted, CanDownloadDataCommandExecute);

        private bool CanDownloadDataCommandExecute(object p) => true;
        private void OnDownloadDataCommandExecuted(object p)
        {
            
        }





        public MainWindowViewModel(IDataCreator dataCreator)
        {


            _dataCreator = dataCreator;
            SqlConnection connection = new SqlConnection(_connectionOrdersString);
            Task.Run(() =>
            {
                _dataCreator.CreateCustomers(_connectionCustomersString);
                _dataCreator.CreateOrders(_connectionOrdersString);
            });

        }


    }
}
