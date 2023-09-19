using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Theme_16.Models;
using Theme_16.Service;

namespace Theme_16.Services
{
    internal class UserDataCreator 
    {
        private string _connectionString;
        IEnumerable<Person> _testCustomers;
        IEnumerable<Order> _orders;
        
        Random _random = new Random();

        public UserDataCreator(string connectionString) 
        { 
            _connectionString = connectionString;
        }

        /// <summary>
        /// Method creates test customers, creates table in DB and adds customers there
        /// </summary>
        /// <returns></returns>
        public async Task CreateCustomers()
        {            
            _testCustomers = Enumerable.Range(1, 50)
                .Select(i => new Person
                {
                    Name = $"Имя {i}",
                    Patronymic = $"Отчество {i}",
                    Surname = $"Фамилия {i}",
                    Phone = (i * _random.Next(i, 100)),
                    Mail = $"mail{i}.@mail.ru"
                });


            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                Debug.WriteLine("Opend connection to DB (create customers)");

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE Customers (Id INT PRIMARY KEY IDENTITY, " +
                    "Name NVARCHAR(100) NOT NULL, " +
                    "Patronymic NVARCHAR(100) NOT NULL, " +
                    "Surname NVARCHAR(100) NOT NULL, " +
                    "Phone INT, " +
                    "Mail NVARCHAR(100) NOT NULL" +
                    ")";
                command.Connection = connection;

                await command.ExecuteNonQueryAsync();
                Debug.WriteLine("Creating customers table");

                foreach(Person person in _testCustomers)
                {
                    command.CommandText = $"INSERT INTO Customers (Name, Patronymic, Surname, Phone, Mail) " +
                        $"VALUES ('{person.Name}', '{person.Patronymic}', '{person.Surname}', {person.Phone}, '{person.Mail}')";

                    await command.ExecuteNonQueryAsync();
                }

                Debug.WriteLine("Added data to customers table");
            }

        }


        /// <summary>
        /// Method creates test orders, creates table in DB and adds orders there
        /// </summary>
        /// <returns></returns>
        public async Task CreateOrders()
        {

            var mails = _testCustomers.Select(i => i.Mail).ToArray();

            _orders = Enumerable.Range(1, 100)
                .Select(i => new Order
                {
                    ItemName = $"Имя товара {i}",
                    ItemCode = _random.Next(0, 100),
                    Mail = _random.NextItem(mails)
                });

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Debug.WriteLine("Opend connection to DB (create orders)");

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE Orders (Id INT PRIMARY KEY IDENTITY, " +
                    "Mail NVARCHAR(100) NOT NULL, " +
                    "ItemCode INT NOT NULL, " +
                    "ItemName NVARCHAR(100) NOT NULL" +
                    ")";
                command.Connection = connection;

                await command.ExecuteNonQueryAsync();
                Debug.WriteLine("Creating orders table");


                foreach(Order order in _orders)
                {
                    command.CommandText = $"INSERT INTO Orders (Mail, ItemCode, ItemName) " +
                        $"VALUES ('{order.Mail}', {order.ItemCode}, '{order.ItemName}')";

                    await command.ExecuteNonQueryAsync();
                }

                Debug.WriteLine("Added data to customers table");
            }

        }
    }
}
