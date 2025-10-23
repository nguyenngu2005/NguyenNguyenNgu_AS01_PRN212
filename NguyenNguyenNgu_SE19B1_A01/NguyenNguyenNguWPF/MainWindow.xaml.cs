// Đảm bảo bạn có 3 using này
using NguyenNguyenNguWPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace NguyenNguyenNguWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow(string email, bool isAdmin)
        {
            InitializeComponent();
            this.DataContext = new DashboardViewModel(email, isAdmin, this.MainFrame);
        }
        private void DragMove_Handler(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized
                                ? WindowState.Normal
                                : WindowState.Maximized;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}