using System.Windows;
using System.Windows.Input;

namespace Theme_16
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }     
    }
}
