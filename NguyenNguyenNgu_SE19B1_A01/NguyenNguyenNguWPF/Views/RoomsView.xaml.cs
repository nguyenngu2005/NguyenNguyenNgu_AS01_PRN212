using System.Windows.Controls;
using NguyenNguyenNguWPF.ViewModels;

namespace NguyenNguyenNguWPF.Views
{
    public partial class RoomsView : UserControl
    {
        public RoomsView()
        {
            InitializeComponent();
            DataContext = new RoomsViewModel();
        }
    }
}
