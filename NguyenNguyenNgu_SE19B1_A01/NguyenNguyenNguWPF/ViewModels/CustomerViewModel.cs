using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Business.Services;
using Data.Entities;
using NguyenNguyenNguWPF.Commands;
using NguyenNguyenNguWPF.Views; // Đảm bảo using này đúng
using System; // Thêm using này cho StringComparison

namespace NguyenNguyenNguWPF.ViewModels
{
    public class CustomersViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _service = new();
        private ObservableCollection<Customer> _customers = new();
        private Customer? _selectedCustomer;
        private string _searchKeyword = string.Empty;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set { _customers = value; OnPropertyChanged(); }
        }

        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string SearchKeyword
        {
            get => _searchKeyword;
            // Tự động tìm kiếm khi gõ chữ (nếu muốn)
            // set { _searchKeyword = value; OnPropertyChanged(); Search(null); }
            set { _searchKeyword = value; OnPropertyChanged(); } // Hoặc giữ nguyên nếu muốn nhấn nút Search
        }


        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }


        public CustomersViewModel()
        {
            LoadCustomers();

            SearchCommand = new RelayCommand(Search);
            AddCommand = new RelayCommand(AddCustomer);
            EditCommand = new RelayCommand(EditCustomer, CanEditDeleteCustomer);
            DeleteCommand = new RelayCommand(DeleteCustomer, CanEditDeleteCustomer);
        }

        private void LoadCustomers()
        {
            // Lấy dữ liệu và sắp xếp theo tên
            Customers = new ObservableCollection<Customer>(_service.GetAll().OrderBy(c => c.CustomerFullName));
            SelectedCustomer = null;
        }

        private void Search(object? obj)
        {
            var allCustomers = _service.GetAll(); // Lấy danh sách gốc
            IEnumerable<Customer> filteredCustomers;

            if (string.IsNullOrWhiteSpace(SearchKeyword))
            {
                filteredCustomers = allCustomers; // Hiển thị tất cả nếu không tìm kiếm
            }
            else
            {
                string keyword = SearchKeyword.Trim();
                // Lọc theo tên, email, hoặc SĐT (sử dụng service đã sửa)
                filteredCustomers = _service.Search(keyword);
                // --- HOẶC --- Lọc trực tiếp ở đây nếu Service chưa sửa
                /*
                filteredCustomers = allCustomers.Where(c =>
                    (c.CustomerFullName?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (c.EmailAddress?.Contains(keyword, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (c.Telephone?.Contains(keyword) ?? false)
                );
                */
            }

            // Cập nhật UI và sắp xếp
            Customers = new ObservableCollection<Customer>(filteredCustomers.OrderBy(c => c.CustomerFullName));
            SelectedCustomer = null; // Bỏ chọn sau khi tìm kiếm
        }

        private void AddCustomer(object? obj)
        {
            // Mở dialog ở chế độ "Add"
            var addEditView = new AddEditCustomerView();
            bool? result = addEditView.ShowDialog();

            if (result == true)
            {
                LoadCustomers(); // Tải lại nếu lưu thành công
            }
        }

        private void EditCustomer(object? obj)
        {
            if (SelectedCustomer == null) return;

            // Mở dialog ở chế độ "Edit" và TRUYỀN SelectedCustomer vào
            var addEditView = new AddEditCustomerView(SelectedCustomer); // <-- ĐÃ SỬA
            bool? result = addEditView.ShowDialog();

            if (result == true)
            {
                LoadCustomers(); // Tải lại nếu lưu thành công
            }
        }

        private void DeleteCustomer(object? obj)
        {
            if (SelectedCustomer == null) return;

            MessageBoxResult confirm = MessageBox.Show(
                $"Are you sure you want to delete customer '{SelectedCustomer.CustomerFullName}' (ID: {SelectedCustomer.CustomerID})?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    _service.Delete(SelectedCustomer.CustomerID);
                    LoadCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CanEditDeleteCustomer(object? obj)
        {
            return SelectedCustomer != null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}