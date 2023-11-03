using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Theme_16.Data;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.Models;
using Theme_16.ModelViews.Base;
using Theme_16.Services;
using Theme_16.Services.Interfaces;

namespace Theme_16.ViewModels
{
    internal class MainViewModel : ViewModel, IDisposable
    {
        private SqlConnection _customersConnection = new SqlConnection(ConnectionStore.ConnectionDB);
        private IDataCreator _dataCreator;

        private ObservableCollection<Person> _customers;
        public ObservableCollection<Person> Customers
        {
            get => _customers;
            private set => Set(ref _customers, value);
        }

        private List<Order> _orders;
        public List<Order> Orders
        {
            get => _orders;
            private set => Set(ref _orders, value);
        }

        public MainViewModel()
        {
            DownloadCustomersData();
        }

        private ICommand _addCommand;
        public ICommand AddCommand => _addCommand ??=
            new LambdaCommand(OnAddCommandExecuted, CanAddCommandExecute);

        private bool CanAddCommandExecute(object p)
        {
            return true;
        }
        private void OnAddCommandExecuted(object p)
        {

        }

        private async Task DownloadCustomersData()
        {
            _customers = new ObservableCollection<Person>();
            using (_customersConnection)
            {
                await _customersConnection.OpenAsync();

                string sqlExpression = "SELECT * FROM Customers";

                SqlCommand command = new SqlCommand(sqlExpression, _customersConnection);

                try
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string patronymic = reader.GetString(2);
                        string surname = reader.GetString(3);
                        int phone = reader.GetInt32(4);
                        string mail = reader.GetString(5);
                        Customers.Add(new Person()
                        {
                            Id = id,
                            Name = name,
                            Patronymic = patronymic,
                            Surname = surname,
                            Phone = phone,
                            Mail = mail
                        });
                        Debug.WriteLine("Added new customer");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine("Чтение не удалось!");
                }
            }

        }

        public void Dispose()
        {
            _customersConnection.Dispose();

        }

    }
}
