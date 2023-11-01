using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Theme_16.Models;
using Theme_16.ModelViews.Base;
using Theme_16.Services.Interfaces;

namespace Theme_16.ViewModels
{
    internal class MainViewModel : ViewModel, IDisposable
    {
        private readonly string _connectionCustomersString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Barbarossa\1_C#\16\Theme_16\Theme_16\Data\CustomersDB.mdf;Integrated Security=True";
        private readonly string _connectionOrdersString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\Barbarossa\1_C#\16\Theme_16\Theme_16\Data\OrdersDB.mdf;Integrated Security=True";



        private SqlConnection _customersConnection;

        private IDataCreator _dataCreator;

        private List<Person> _customers = new List<Person>();
        public List<Person> Customers
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

        public MainViewModel(IDataCreator dataCreator)
        {
            _customersConnection = new SqlConnection(_connectionCustomersString);

            
            try
            {
                _ = DownloadCustomersData();
            }
            catch (Exception ex)
            {
                _dataCreator = dataCreator;
                SqlConnection connection = new SqlConnection(_connectionOrdersString);
                Task.Run(() =>
                {
                    _dataCreator.CreateCustomers(_connectionCustomersString);
                    _dataCreator.CreateOrders(_connectionOrdersString);
                });

                Debug.WriteLine("Kind of finished creating DB");
                _ = DownloadCustomersData();
            }
        }

        private async Task DownloadCustomersData()
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

                    _customers.Add(new Person()
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
                OnPropertyChanged(nameof(Customers));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("Чтение не удалось!");
            }
        }

        public void Dispose()
        {
            _customersConnection.Close();
            _customersConnection.Dispose();
        }

    }
}
