using NguyenNguyenNguWPF.ViewModels; // <-- Using ViewModel
using System.Windows.Controls;

namespace NguyenNguyenNguWPF.Views // <-- Namespace phải khớp
{
    public partial class CustomerWelcomeView : UserControl // <-- Kế thừa từ UserControl
    {
        public CustomerWelcomeView()
        {
            InitializeComponent(); // <-- Cần thiết để tải XAML
            DataContext = new CustomerWelcomeViewModel(); // <-- Gán ViewModel
        }
    }
}