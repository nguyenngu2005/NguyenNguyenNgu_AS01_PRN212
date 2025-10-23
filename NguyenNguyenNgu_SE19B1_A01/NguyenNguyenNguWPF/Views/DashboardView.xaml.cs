using System.Windows.Controls;
using NguyenNguyenNguWPF.ViewModels; 

namespace NguyenNguyenNguWPF.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardPageViewModel();
        }
    }
}