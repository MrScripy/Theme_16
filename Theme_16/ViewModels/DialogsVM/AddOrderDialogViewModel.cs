using System.Windows.Input;
using Theme_16.Infrastrucutre.Commands;
using Theme_16.Models;
using Theme_16.ModelViews.Base;
using Theme_16.Services;
using System;

namespace Theme_16.ViewModels.DialogsVM
{
    internal class AddOrderDialogViewModel : ViewModel
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        private readonly TransferOrderService _transferOrderService;

        public AddOrderDialogViewModel(TransferOrderService transferOrderService)
        {
            _transferOrderService = transferOrderService;
        }

        //Add Order
        private ICommand _addOrderCommand;
        public ICommand AddOrderCommand => _addOrderCommand ??=
            new LambdaCommand(OnAddOrderCommandExecuted, CanAddOrderCommandExecute);

        private bool CanAddOrderCommandExecute(object p)
        {
            if (!string.IsNullOrEmpty(ItemCode) && !string.IsNullOrEmpty(ItemName))
            {
                if (int.TryParse(ItemCode, out _))
                    return true;
            }
            return false;
        }
        private void OnAddOrderCommandExecuted(object p)
        {
            _transferOrderService.Order = new Order()
            {
                ItemCode = Int32.Parse(ItemCode),
                ItemName = ItemName
            };

            App.CurrentWindow.Close();
        }

    }
}
