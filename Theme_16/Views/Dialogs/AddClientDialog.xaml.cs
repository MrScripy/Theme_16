using System.Windows;
using System.Windows.Input;
using Theme_16.Models;

namespace Theme_16.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddClientDialog.xaml
    /// </summary>
    public partial class AddClientDialog : Window
    {
        public AddClientDialog()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

       

    }
}
