using NguyenNguyenNguWPF.ViewModels;
using System.Windows;
using System.Windows.Input;
using Data.Entities; 
using System;

namespace NguyenNguyenNguWPF.Views
{
    public partial class AddEditCustomerView : Window
    {
        private AddEditCustomerViewModel _viewModel;

        public AddEditCustomerView()
        {
            InitializeComponent();
            _viewModel = new AddEditCustomerViewModel(null, CloseWindow); // CloseWindow được gọi từ đây
            this.DataContext = _viewModel;
        }

        public AddEditCustomerView(Customer customerToEdit)
        {
            InitializeComponent();
            _viewModel = new AddEditCustomerViewModel(customerToEdit, CloseWindow); // CloseWindow được gọi từ đây
            this.DataContext = _viewModel;
        }

        private void CloseWindow(bool dialogResult)
        {
            this.DialogResult = dialogResult;
            this.Close();
        }

        private void DragMove_Handler(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
        }

    } 
}