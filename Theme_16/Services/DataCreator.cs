﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Theme_16.Models;
using Theme_16.Service;
using Theme_16.Services.Interfaces;
using Theme_16.Data;

namespace Theme_16.Services
{
    internal class DataCreator : IDataCreator
    {
        IEnumerable<Person> _testCustomers;
        IEnumerable<Order> _orders;

        Random _random = new Random();


        public async Task FillDB()
        {
            await GenerateData();


            using (SqlConnection connection = new SqlConnection(ConnectionStore.ConnectionDB))
            {
                await connection.OpenAsync();

                //await CreateCustomersTable(connection);
                //await CreateOrdersTable(connection);

                await CleanDB(connection);
                await CreateCustomersData(connection);
                await CreateOrdersData(connection);
                connection.Close();
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

        private async Task CleanDB(SqlConnection connection)
        {

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "TRUNCATE TABLE Customers" +
                "TRUNCATE TABLE Orders";
            command.Connection = connection;

            await command.ExecuteNonQueryAsync();
            Debug.WriteLine("created DB or cleaned and recreated DB");
        }

        /// <summary>
        /// Method creates test customers, creates table in DB and adds customers there
        /// </summary>
        /// <returns></returns>
        private async Task CreateCustomersData(SqlConnection connection)
        {
            Debug.WriteLine("Opend connection to DB (create customers)");

            SqlCommand command = connection.CreateCommand();

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
        private async Task CreateOrdersData(SqlConnection connection)
        {
            Debug.WriteLine("Opend connection to DB (create orders)");

            SqlCommand command = connection.CreateCommand();

            foreach (Order order in _orders)
            {
                command.CommandText = $"INSERT INTO Orders (Mail, ItemCode, ItemName) " +
                    $"VALUES ('{order.Mail}', {order.ItemCode}, '{order.ItemName}')";

                await command.ExecuteNonQueryAsync();
            }

            Debug.WriteLine("Added data to customers table");

        }

        private async Task CreateCustomersTable(SqlConnection connection)
        {
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

        }
        private async Task CreateOrdersTable(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE Orders (Id INT PRIMARY KEY IDENTITY, " +
                "Mail NVARCHAR(100) NOT NULL, " +
                "ItemCode INT NOT NULL, " +
                "ItemName NVARCHAR(100) NOT NULL" +
                ")";
            command.Connection = connection;

            await command.ExecuteNonQueryAsync();
            Debug.WriteLine("Creating orders table");
        }
    }
}
