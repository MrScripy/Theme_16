using System.Windows;
using System.Windows.Input;
using Theme_16.Views;

namespace Theme_16
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddWindow window = new AddWindow(); 
            window.Show();
        }
    }
}
