using NguyenNguyenNguWPF.Helpers; 
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Business.Services;

namespace NguyenNguyenNguWPF.ViewModels
{
    public class CustomerWelcomeViewModel : INotifyPropertyChanged
    {

        private readonly CustomerService _customerService = new();

        private string _welcomeMessage = "Welcome!"; 

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set { if (_welcomeMessage != value) { _welcomeMessage = value; OnPropertyChanged(); } }
        }

        public CustomerWelcomeViewModel()
        {
            string? currentEmail = UserSession.CurrentEmail;
            if (!string.IsNullOrEmpty(currentEmail))
            {
                WelcomeMessage = $"Welcome, {currentEmail}!";
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}