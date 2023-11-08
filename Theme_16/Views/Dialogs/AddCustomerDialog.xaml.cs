using System.Windows;
using System.Windows.Input;
using Theme_16.Models;

namespace Theme_16.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddCustomerDialog.xaml
    /// </summary>
    public partial class AddCustomerDialog : Window
    {
        public AddCustomerDialog()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

       

    }
}
