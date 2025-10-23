using Business.Services;
using Data.Entities;
using NguyenNguyenNguWPF.Commands;
using NguyenNguyenNguWPF.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace NguyenNguyenNguWPF.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService = new();
        private readonly BookingService _bookingService = new();

        private Customer? _originalCustomer;
        private Customer _currentCustomer = new();
        private string _newPassword = string.Empty;
        private ICollectionView? _bookingHistoryView;
        private string _profileMessage = string.Empty;
        private Brush _messageColor = Brushes.Green;

        public Customer CurrentCustomer
        {
            get => _currentCustomer;
            set { _currentCustomer = value; OnPropertyChanged(); }
        }

        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(); }
        }

        public ICollectionView? BookingHistoryView
        {
            get => _bookingHistoryView;
            set { _bookingHistoryView = value; OnPropertyChanged(); }
        }

        public string ProfileMessage
        {
            get => _profileMessage;
            set { _profileMessage = value; OnPropertyChanged(); }
        }
        public Brush MessageColor
        {
            get => _messageColor;
            set { _messageColor = value; OnPropertyChanged(); }
        }

        public ICommand UpdateProfileCommand { get; }

        public ProfileViewModel()
        {
            LoadCurrentCustomer();
            LoadBookingHistory();
            UpdateProfileCommand = new RelayCommand(UpdateProfile, CanUpdateProfile);
        }

        private void LoadCurrentCustomer()
        {
            string? currentEmail = UserSession.CurrentEmail;
            if (string.IsNullOrEmpty(currentEmail))
            {
                ProfileMessage = "Error: Could not find logged-in user email.";
                MessageColor = Brushes.OrangeRed;
                return;
            }

            _originalCustomer = _customerService.GetByEmail(currentEmail);

            if (_originalCustomer != null)
            {
                CurrentCustomer = new Customer
                {
                    CustomerID = _originalCustomer.CustomerID,
                    CustomerFullName = _originalCustomer.CustomerFullName,
                    EmailAddress = _originalCustomer.EmailAddress, 
                    Telephone = _originalCustomer.Telephone,
                    Password = _originalCustomer.Password,
                    CustomerBirthday = _originalCustomer.CustomerBirthday,
                    CustomerStatus = _originalCustomer.CustomerStatus
                };
            }
            else
            {
                ProfileMessage = "Error: Could not load customer data.";
                MessageColor = Brushes.OrangeRed;
            }
        }

        private void LoadBookingHistory()
        {
            if (_originalCustomer == null) return;
            var history = _bookingService.GetAllBookings()
                                        .Where(b => b.CustomerID == _originalCustomer.CustomerID)
                                        .OrderByDescending(b => b.BookingDate)
                                        .ToList();
            BookingHistoryView = CollectionViewSource.GetDefaultView(history);
        }

        private bool CanUpdateProfile(object? obj)
        {
            return CurrentCustomer != null &&
                   !string.IsNullOrWhiteSpace(CurrentCustomer.CustomerFullName) &&
                   !string.IsNullOrWhiteSpace(CurrentCustomer.Telephone) &&
                   !string.IsNullOrWhiteSpace(CurrentCustomer.EmailAddress);
        }

        private void UpdateProfile(object? obj)
        {
            if (!CanUpdateProfile(null) || _originalCustomer == null)
            {
                ProfileMessage = "Please fill in required fields (Name, Telephone, Email)."; // Cập nhật thông báo
                MessageColor = Brushes.OrangeRed;
                return;
            }
            var existingEmailUser = _customerService.GetByEmail(CurrentCustomer.EmailAddress);
            if (existingEmailUser != null && existingEmailUser.CustomerID != _originalCustomer.CustomerID)
            {
                ProfileMessage = "This email address is already used by another account.";
                MessageColor = Brushes.OrangeRed;
                return;
            }

            _originalCustomer.CustomerFullName = CurrentCustomer.CustomerFullName;
            _originalCustomer.Telephone = CurrentCustomer.Telephone;
            _originalCustomer.CustomerBirthday = CurrentCustomer.CustomerBirthday;
            _originalCustomer.EmailAddress = CurrentCustomer.EmailAddress;

            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                _originalCustomer.Password = NewPassword;
            }

            try
            {
                _customerService.Update(_originalCustomer);
                if (UserSession.CurrentEmail != _originalCustomer.EmailAddress)
                {
                    UserSession.CurrentEmail = _originalCustomer.EmailAddress;
                }

                ProfileMessage = "Profile updated successfully!";
                MessageColor = Brushes.LightGreen;
                NewPassword = string.Empty; 
                OnPropertyChanged(nameof(CurrentCustomer));
            }
            catch (Exception ex)
            {
                ProfileMessage = $"Error updating profile: {ex.Message}";
                MessageColor = Brushes.OrangeRed;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            (UpdateProfileCommand as RelayCommand)?.RaiseCanExecuteChanged(); 
        }
    }
}