using System.Windows;
using System.Windows.Input;

namespace Theme_16.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для ChangeClientInfoDialog.xaml
    /// </summary>
    public partial class ChangeClientInfoDialog : Window
    {
        public ChangeClientInfoDialog()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
