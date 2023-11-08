using System.Windows;
using System.Windows.Input;

namespace Theme_16.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddOrderDialog.xaml
    /// </summary>
    public partial class AddOrderDialog : Window
    {
        public AddOrderDialog() => InitializeComponent();

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
