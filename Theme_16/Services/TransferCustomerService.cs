using System.Collections.Generic;
using Theme_16.Models;

namespace Theme_16.Services
{
    internal class TransferCustomerService
    {
        private Person _customer = new Person();
        public Person Customer
        {
            get => _customer;
            set => _customer = value;
        }
    }
}
