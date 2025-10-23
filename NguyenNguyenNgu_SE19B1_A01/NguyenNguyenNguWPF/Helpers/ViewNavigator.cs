using System;
using System.Windows.Controls;

namespace NguyenNguyenNguWPF.Helpers
{
    public static class ViewNavigator
    {
        public static void Navigate(Frame frame, UserControl view)
        {
            if (frame == null || view == null) return;
            frame.Content = view;
        }
    }
}
