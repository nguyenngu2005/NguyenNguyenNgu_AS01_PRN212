using NguyenNguyenNguWPF.Commands; 
using NguyenNguyenNguWPF.ViewModels;
using System.Windows;
using System.Windows.Controls; 
using System.Windows.Input; 
using NguyenNguyenNguWPF.Helpers; 


namespace NguyenNguyenNguWPF.Views 
{
    public partial class LoginView : Window
    {
        private LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel(); 
            this.DataContext = _viewModel; 

        }
        private void DragMove_Handler(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); 
        }
        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel.LoginCommand is RelayCommand relayCommand)
            {
                relayCommand.RaiseCanExecuteChanged();
            }
        }
    }
}