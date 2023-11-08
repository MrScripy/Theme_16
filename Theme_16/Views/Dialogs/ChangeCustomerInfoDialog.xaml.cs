using System.Windows;
using System.Windows.Input;

namespace Theme_16.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ChangeCustomerInfoDialog.xaml
    /// </summary>
    public partial class ChangeCustomerInfoDialog : Window
    {
        public ChangeCustomerInfoDialog()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
