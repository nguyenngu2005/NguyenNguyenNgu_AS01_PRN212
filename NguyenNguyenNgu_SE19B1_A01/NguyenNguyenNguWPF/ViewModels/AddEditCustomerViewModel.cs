using Business.Services;
using Data.Entities;
using NguyenNguyenNguWPF.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NguyenNguyenNguWPF.ViewModels
{
    public class AddEditCustomerViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService = new();
        private Customer _currentCustomer = new();

        private bool _isEditMode;
        private string _windowTitle = string.Empty;
        private string _errorMessage = string.Empty;
        private Action<bool> _closeWindowAction; 

        public Customer CurrentCustomer
        {
            get => _currentCustomer;
            set { if (_currentCustomer != value) { _currentCustomer = value; OnPropertyChanged(); } }
        }

        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(); }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditCustomerViewModel(Customer? customerToEdit, Action<bool> closeWindowAction)
        {
            _closeWindowAction = closeWindowAction;

            if (customerToEdit == null) 
            {
                _isEditMode = false;
                WindowTitle = "Add New Customer";
                CurrentCustomer = new Customer
                {
                    CustomerBirthday = DateTime.Now.AddYears(-20),
                    CustomerStatus = 1 
                };
            }
            else 
            {
                _isEditMode = true;
                WindowTitle = $"Edit Customer - {customerToEdit.CustomerFullName}";
                CurrentCustomer = new Customer
                {
                    CustomerID = customerToEdit.CustomerID,
                    CustomerFullName = customerToEdit.CustomerFullName,
                    EmailAddress = customerToEdit.EmailAddress,
                    Telephone = customerToEdit.Telephone,
                    Password = customerToEdit.Password,
                    CustomerBirthday = customerToEdit.CustomerBirthday,
                    CustomerStatus = customerToEdit.CustomerStatus
                };
            }

            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
        }

        private bool CanSave(object? obj)
        {
            if (CurrentCustomer == null) return false;

            return !string.IsNullOrWhiteSpace(CurrentCustomer.CustomerFullName) &&
                   !string.IsNullOrWhiteSpace(CurrentCustomer.EmailAddress) &&
                   !string.IsNullOrWhiteSpace(CurrentCustomer.Password);
        }

        private void Save(object? obj)
        {
            if (CurrentCustomer == null || !CanSave(null))
            {
                ErrorMessage = "Please fill in all required fields (Name, Email, Password).";
                return;
            }

            ErrorMessage = string.Empty;
            try
            {
                if (_isEditMode)
                {
                    _customerService.Update(CurrentCustomer);
                }
                else
                {
                    _customerService.Add(CurrentCustomer);
                }

                _closeWindowAction?.Invoke(true);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error saving customer: {ex.Message}";
            }
        }

        private void Cancel(object? obj)
        {
            _closeWindowAction?.Invoke(false);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }
}