using System.Windows;

namespace NguyenNguyenNguWPF.Views
{
    public partial class AddEditRoomWindow : Window
    {
        public AddEditRoomWindow()
        {
            InitializeComponent();

            Loaded += (_, __) =>
            {
                if (DataContext is AddEditRoomViewModel vm)
                    vm.CloseAction = (ok) => { DialogResult = ok; Close(); };
            };
        }
    }
}
