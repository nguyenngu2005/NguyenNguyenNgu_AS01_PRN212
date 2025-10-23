using NguyenNguyenNguWPF.ViewModels;
using System.Windows.Controls;

namespace NguyenNguyenNguWPF.Views
{
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
            DataContext = new ProfileViewModel(); 
        }
    }
}