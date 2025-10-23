using System.Windows.Controls;
using NguyenNguyenNguWPF.ViewModels;

namespace NguyenNguyenNguWPF.Views
{
    public partial class ReportView : UserControl
    {
        public ReportView()
        {
            InitializeComponent();
            DataContext = new ReportViewModel();
        }
    }
}
