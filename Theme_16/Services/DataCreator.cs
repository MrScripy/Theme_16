using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Theme_16.Models;
using Theme_16.Service;
using Theme_16.Services.Interfaces;

namespace Theme_16.Services
{
    internal class DataCreator : IDataCreator
    {
        // string connectionString = @"Server=(localdb)\mssqllocaldb;Database=master;Trusted_Connection=True";

        private string _connectionCustomersString;
        private string _connectionOrdersString;

        IEnumerable<Person> _testCustomers;
        IEnumerable<Order> _orders;

        Random _random = new Random();

        public DataCreator(string connectionCustomersString, string connectionOrdersString)
        {
            _connectionCustomersString = connectionCustomersString;
            _connectionOrdersString = connectionOrdersString;
        }


        public async Task FillDB()
        {
            await GenerateData();
            using (SqlConnection connection = new SqlConnection(_connectionCustomersString))
            {
                await CreateCustomers(connection);
            }
            using (SqlConnection connection = new SqlConnection(_connectionOrdersString))
            {
                await CreateOrders(connection);
            }
        }

        private async Task GenerateData()
        {
            _testCustomers = Enumerable.Range(1, 50)
              .Select(i => new Person
              {
                  Name = $"Name {i}",
                  Patronymic = $"Patronymic {i}",
                  Surname = $"Surname {i}",
                  Phone = (i * _random.Next(i, 100)),
                  Mail = $"mail{i}.@mail.ru"
              });

            var mails = _testCustomers.Select(i => i.Mail).ToArray();

            _orders = Enumerable.Range(1, 100)
                .Select(i => new Order
                {
                    ItemName = $"Item name {i}",
                    ItemCode = _random.Next(0, 100),
                    Mail = _random.NextItem(mails)
                });
        }


        /// <summary>
        /// Method creates test customers, creates table in DB and adds customers there
        /// </summary>
        /// <returns></returns>
        private async Task CreateCustomers(SqlConnection connection)
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

            foreach (Person person in _testCustomers)
            {
                command.CommandText = $"INSERT INTO Customers (Name, Patronymic, Surname, Phone, Mail) " +
                    $"VALUES ('{person.Name}', '{person.Patronymic}', '{person.Surname}', {person.Phone}, '{person.Mail}')";

                await command.ExecuteNonQueryAsync();
            }

            Debug.WriteLine("Added data to customers table");

        }


        /// <summary>
        /// Method creates test orders, creates table in DB and adds orders there
        /// </summary>
        /// <returns></returns>
        private async Task CreateOrders(SqlConnection connection)
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


            foreach (Order order in _orders)
            {
                command.CommandText = $"INSERT INTO Orders (Mail, ItemCode, ItemName) " +
                    $"VALUES ('{order.Mail}', {order.ItemCode}, '{order.ItemName}')";

                await command.ExecuteNonQueryAsync();
            }

            Debug.WriteLine("Added data to customers table");

        }
    }
}
