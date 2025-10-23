using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Business.Services;
using Microsoft.Extensions.Configuration;
using NguyenNguyenNguWPF.Commands;
using NguyenNguyenNguWPF.Helpers; 
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System; 
using System.Linq; 

namespace NguyenNguyenNguWPF.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService = new();

        private string _email = string.Empty;
        private string _errorMessage = string.Empty;
        private string _password = string.Empty; 

        private readonly string _adminEmail = "";
        private readonly string _adminPassword = "";

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged(); }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); } 
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                var config = builder.Build();
                _adminEmail = config["Admin:Email"] ?? string.Empty;
                _adminPassword = config["Admin:Password"] ?? string.Empty;

                if (string.IsNullOrEmpty(_adminEmail) || string.IsNullOrEmpty(_adminPassword))
                {
                    System.Diagnostics.Debug.WriteLine("Cảnh báo: Thông tin admin trong appsettings.json bị thiếu!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi đọc appsettings.json: {ex.Message}");
                ErrorMessage = "Lỗi cấu hình hệ thống.";
                _adminEmail = "admin@error.com";
                _adminPassword = "error";
            }

            LoginCommand = new RelayCommand(Login, CanLogin);
        }
        private bool CanLogin(object? parameter)
        {
            if (parameter is PasswordBox pwdBox)
            {
                return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(pwdBox.Password);
            }
            return false; 
        }


        private void Login(object? parameter)
        {
            if (parameter is not PasswordBox pwdBox) return;
            string password = pwdBox.Password; 
            ErrorMessage = string.Empty; 

            bool isAdminLogin = false;
            string loggedInEmail = string.Empty;

            if (!string.IsNullOrEmpty(_adminEmail) &&
                Email.Equals(_adminEmail, StringComparison.OrdinalIgnoreCase) &&
                password == _adminPassword)
            {
                isAdminLogin = true;
                loggedInEmail = _adminEmail;
            }
            else 
            {
                var account = _customerService.GetByEmail(Email);
                if (account != null && account.Password == password)
                {
                    isAdminLogin = false; 
                    loggedInEmail = account.EmailAddress;
                }
            }

            if (!string.IsNullOrEmpty(loggedInEmail)) 
            {
                UserSession.CurrentEmail = loggedInEmail;
                UserSession.IsAdmin = isAdminLogin;

                var mainWindow = new MainWindow(loggedInEmail, isAdminLogin);
                mainWindow.Show();

                CloseLoginWindow();
            }
            else 
            {
                ErrorMessage = "Invalid email or password.";
            }
        }

        private static void CloseLoginWindow()
        {
            Window? loginWindow = Application.Current.Windows.OfType<Views.LoginView>().FirstOrDefault();
            loginWindow?.Close(); 
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}