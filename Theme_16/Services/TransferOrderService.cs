using Theme_16.Models;

namespace Theme_16.Services
{
    internal class TransferOrderService
    {
        private Order _order;
        public Order Order
        {
            get => _order;
            set => _order = value;
        }
    }
}
