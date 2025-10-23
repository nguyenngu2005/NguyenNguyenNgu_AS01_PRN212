using System.Windows.Controls;
using System.Windows.Input;
using Business.Services;
using NguyenNguyenNguWPF.Commands;
using NguyenNguyenNguWPF.Helpers;
using NguyenNguyenNguWPF.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Linq;

namespace NguyenNguyenNguWPF.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService = new();
        private readonly Frame _mainFrame;
        private string _currentUserName = "Unknown";
        private bool _isAdmin;

        public string CurrentUserName
        {
            get => _currentUserName;
            set { _currentUserName = value; OnPropertyChanged(); }
        }

        public bool IsAdmin => _isAdmin;
        public bool IsCustomer => !_isAdmin;

        public ICommand NavigateCommand { get; }
        public ICommand LogoutCommand { get; }

        public DashboardViewModel(string email, bool isAdmin, Frame mainFrame)
        {
            _mainFrame = mainFrame;
            _isAdmin = isAdmin;

            var user = _customerService.GetByEmail(email);
            if (user != null)
                CurrentUserName = $"Welcome, {user.CustomerFullName}";
            else if (isAdmin)
                CurrentUserName = $"Welcome, Administrator";
            NavigateCommand = new RelayCommand(Navigate);
            LogoutCommand = new RelayCommand(Logout);

            if (_isAdmin)
            {
                ViewNavigator.Navigate(_mainFrame, new DashboardView());
            }
            else
            {
                ViewNavigator.Navigate(_mainFrame, new CustomerWelcomeView());
            }
        }

        private void Navigate(object? parameter)
        {
            string? viewName = parameter?.ToString();
            if (string.IsNullOrEmpty(viewName)) return;

            switch (viewName)
            {
                case "Dashboard": // Chỉ Admin mới có nút này
                    if (IsAdmin)
                        ViewNavigator.Navigate(_mainFrame, new DashboardView());
                    break;
                case "Profile": // Chỉ Customer
                    if (IsCustomer)
                        ViewNavigator.Navigate(_mainFrame, new ProfileView());
                    else
                        MessageBox.Show("Admin profile is managed elsewhere.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "Customers": // Chỉ Admin
                    if (IsAdmin)
                        ViewNavigator.Navigate(_mainFrame, new CustomersView());
                    else
                        MessageBox.Show("Access Denied. Admin rights required.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "Rooms": // Cả hai
                    ViewNavigator.Navigate(_mainFrame, new RoomsView());
                    break;
                case "Report": // Chỉ Admin
                    if (IsAdmin)
                        ViewNavigator.Navigate(_mainFrame, new ReportView());
                    else
                        MessageBox.Show("Access Denied. Admin rights required.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }

        private void Logout(object? obj)
        {
            UserSession.Clear();

            var login = new Views.LoginView();
            login.Show();

            Window? mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow?.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}