using System.Collections.Generic;
using Theme_16.Models;

namespace Theme_16.Services
{
    internal class TransferCustomerService
    {
        private Customer _customer = new Customer();
        public Customer Customer
        {
            get => _customer;
            set => _customer = value;
        }
    }
}
