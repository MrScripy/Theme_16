﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Theme_16.Stores;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.Models;
using Theme_16.ModelViews.Base;
using Theme_16.Services;
using Theme_16.Views.Dialogs;

namespace Theme_16.ViewModels
{
    internal class MainViewModel : ViewModel, IDisposable
    {
        #region Properties

        private readonly TransferCustomerService _transferCustomerService;
        private readonly TransferOrderService _transferOrderService;
        private SqlConnection _connection = new SqlConnection(ConnectionStore.ConnectionDB);

        private ObservableCollection<Person> _customers;
        public ObservableCollection<Person> Customers
        {
            get => _customers;
            private set => Set(ref _customers, value);
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            private set => Set(ref _orders, value);
        }

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                Set(ref _selectedPerson, value);
                _transferCustomerService.Customer = SelectedPerson;
            }
        }

        #endregion
        public MainViewModel(TransferCustomerService transferCustomerService, TransferOrderService transferOrderService)
        {
            DownloadCustomersData();
            _transferCustomerService = transferCustomerService;
            _transferOrderService = transferOrderService;
        }

        #region commands
        //Change Customer

        private ICommand _changeCustomerCommand;
        public ICommand ChangeCustomerCommand => _changeCustomerCommand ??=
            new LambdaCommand(OnChangeCustomerCommandExecuted, CanChangeCustomerCommandExecute);

        private bool CanChangeCustomerCommandExecute(object p) => SelectedPerson != null ? true : false;
        private void OnChangeCustomerCommandExecuted(object p)
        {
            _transferCustomerService.Customer = SelectedPerson;

            Window changeClientInfoDialog = new ChangeClientInfoDialog();
            changeClientInfoDialog.ShowDialog();



            try
            {
                _connection.Open();
                SqlCommand command = _connection.CreateCommand();
                command.CommandText = $"UPDATE Customers " +
                        $"SET Name = '{SelectedPerson.Name}', Patronymic = '{SelectedPerson.Patronymic}', Surname = '{SelectedPerson.Surname}', Phone = '{SelectedPerson.Phone}' " +
                        $"WHERE Mail = '{SelectedPerson.Mail}'";

                command.ExecuteNonQuery();
                Debug.WriteLine("Updated Customer");


                // crutch, but didn't find another way to update view (OnPropertyChanged doesn't work)
                var person = Customers.FirstOrDefault<Person>(p => p.Mail == SelectedPerson.Mail);
                if (person != null)
                {
                    Customers.Remove(person);
                    Customers.Insert(person.Id - 1, person);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("adding new customer is failed");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

        }

        //Add Customer
        private ICommand _addCustomerCommand;
        public ICommand AddCustomerCommand => _addCustomerCommand ??=
            new LambdaCommand(OnAddCustomerCommandExecuted, CanAddCustomerCommandExecute);

        private bool CanAddCustomerCommandExecute(object p)
        {
            return true;
        }
        private void OnAddCustomerCommandExecuted(object p)
        {
            Window addClientDialog = new AddClientDialog();
            addClientDialog.ShowDialog();
            Person newCustomer = _transferCustomerService.Customer;

            if(newCustomer == null)
            {
                return;
            }
            else if (CheckPersonUniqueness(newCustomer))
            {
                MessageBox.Show("User with the same e-mail already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (newCustomer != null)
            {
                try
                {
                    _connection.Open();
                    SqlCommand command = _connection.CreateCommand();
                    command.CommandText = $"INSERT INTO Customers (Name, Patronymic, Surname, Phone, Mail) " +
                            $"VALUES ('{newCustomer.Name}', '{newCustomer.Patronymic}', '{newCustomer.Surname}', {newCustomer.Phone}, '{newCustomer.Mail}')";

                    command.ExecuteNonQuery();
                    Debug.WriteLine("Added new Customer");

                    command.CommandText = $"SELECT * FROM Customers WHERE Mail = '{newCustomer.Mail}'";

                    using (SqlDataReader customersReader = command.ExecuteReader())
                    {
                        while (customersReader.Read())
                        {
                            Customers.Add(new Person()
                            {
                                Id = customersReader.GetInt32(0),
                                Name = customersReader.GetString(1),
                                Patronymic = customersReader.GetString(2),
                                Surname = customersReader.GetString(3),
                                Phone = customersReader.GetInt32(4),
                                Mail = customersReader.GetString(5)
                            });

                            Debug.WriteLine("Added new customer");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("adding new customer is failed");
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        //Add Order
        private ICommand _addOrderCommand;
        public ICommand AddOrderCommand => _addOrderCommand ??=
            new LambdaCommand(OnAddOrderCommandExecuted, CanAddOrderCommandExecute);

        private bool CanAddOrderCommandExecute(object p) => SelectedPerson != null ? true : false;
        private void OnAddOrderCommandExecuted(object p)
        {
            Window addOrderDialog = new AddOrderDialog();
            addOrderDialog.ShowDialog();

            Order newOrder = _transferOrderService.Order;
            if (newOrder != null)
            {
                try
                {
                    _connection.Open();
                    SqlCommand command = _connection.CreateCommand();
                    command.CommandText = $"INSERT INTO Orders (Mail, ItemCode, ItemName) " +
                        $"VALUES ('{SelectedPerson.Mail}', {newOrder.ItemCode}, '{newOrder.ItemName}')";
                    command.ExecuteNonQuery();
                    Debug.WriteLine("Added new Order");

                    command.CommandText = $"SELECT * FROM Orders WHERE Mail = '{SelectedPerson.Mail}'";

                    using (SqlDataReader ordersReader = command.ExecuteReader())
                    {
                        while (ordersReader.Read())
                        {
                            SelectedPerson.Orders.Add(new Order()
                            {
                                Id = ordersReader.GetInt32(0),
                                Mail = ordersReader.GetString(1),
                                ItemCode = ordersReader.GetInt32(2),
                                ItemName = ordersReader.GetString(3),
                            });

                            Debug.WriteLine("Added new order");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("adding new order is failed");
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        // Clear Tables
        private ICommand _clearCommand;

        public ICommand ClearCommand => _clearCommand ??=
            new LambdaCommand(OnClearCommandExecuted, CanClearCommandExecute);

        private bool CanClearCommandExecute(object p) => Customers.Count > 0 ? true : false;
        private void OnClearCommandExecuted(object p)
        {
            ClearData();
        }
        #endregion


        #region private methods
        private async Task DownloadCustomersData()
        {
            _customers = new ObservableCollection<Person>();

            try
            {
                await _connection.OpenAsync();
                Customers = await GetCustomersAsync();
                foreach (Person customer in Customers)
                {
                    customer.Orders = await GetOrdersAsync(customer);
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Download Customers Data");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }
        private async Task<ObservableCollection<Person>> GetCustomersAsync()
        {
            ObservableCollection<Person> customers = new ObservableCollection<Person>();
            string sqlCustomersExpression = "SELECT * FROM Customers";

            SqlCommand getCustomersCommand = new SqlCommand(sqlCustomersExpression, _connection);

            try
            {
                using (SqlDataReader customersReader = await getCustomersCommand.ExecuteReaderAsync())
                {
                    while (await customersReader.ReadAsync())
                    {

                        string mail = customersReader.GetString(5);
                        customers.Add(new Person()
                        {
                            Id = customersReader.GetInt32(0),
                            Name = customersReader.GetString(1),
                            Patronymic = customersReader.GetString(2),
                            Surname = customersReader.GetString(3),
                            Phone = customersReader.GetInt32(4),
                            Mail = customersReader.GetString(5)
                        });

                        Debug.WriteLine("Added new customer");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("Чтение не удалось!");
            }
            return customers;
        }
        private async Task<ObservableCollection<Order>> GetOrdersAsync(Person person)
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();

            string sqlOrdersExpression = $"SELECT * FROM Orders WHERE Mail = '{person.Mail}'";
            SqlCommand getOrdersCommand = new SqlCommand(sqlOrdersExpression, _connection);
            try
            {
                using (SqlDataReader ordersReader = await getOrdersCommand.ExecuteReaderAsync())
                {
                    while (await ordersReader.ReadAsync())
                    {
                        orders.Add(new Order()
                        {
                            Id = ordersReader.GetInt32(0),
                            Mail = ordersReader.GetString(1),
                            ItemCode = ordersReader.GetInt32(2),
                            ItemName = ordersReader.GetString(3),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetOrdersAsync");
                Debug.WriteLine(ex.Message);
            }
            return orders;
        }

        private async Task ClearData()
        {
            string sqlExpression = "DELETE FROM Customers " +
                "DELETE FROM Orders";

            try
            {

                await _connection.OpenAsync();

                SqlCommand sqlCommand = new SqlCommand(sqlExpression, _connection);
                await sqlCommand.ExecuteNonQueryAsync();

                Debug.WriteLine("Cleared DB");

                Customers.Clear();


            }
            catch (Exception ex)
            {
                Debug.WriteLine("ClearData");
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        private bool CheckPersonUniqueness(Person person)
        {
            Person checkedPerson = Customers.FirstOrDefault<Person>(person => person.Mail == person.Mail);
            return checkedPerson == null ? true : false;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
        #endregion
    }
}
