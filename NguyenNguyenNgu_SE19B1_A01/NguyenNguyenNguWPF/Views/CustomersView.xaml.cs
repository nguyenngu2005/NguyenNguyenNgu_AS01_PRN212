using System.Windows.Controls;
using NguyenNguyenNguWPF.ViewModels;

namespace NguyenNguyenNguWPF.Views
{
    public partial class CustomersView : UserControl
    {
        public CustomersView()
        {
            InitializeComponent();
            DataContext = new CustomersViewModel();
        }
    }
}
