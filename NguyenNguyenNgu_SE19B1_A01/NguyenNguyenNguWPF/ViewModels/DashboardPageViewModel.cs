using Business.Services; 
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NguyenNguyenNguWPF.ViewModels
{
    public class DashboardPageViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService = new();
        private readonly RoomService _roomService = new();
        private readonly BookingService _bookingService = new();
        public int TotalCustomers { get; set; }
        public int AvailableRooms { get; set; }
        public int BookedRooms { get; set; }
        public decimal TotalRevenue { get; set; }

        public DashboardPageViewModel()
        {
            LoadStats();
        }

        private void LoadStats()
        {
            TotalCustomers = _customerService.GetAll().Count(c => c.CustomerID != 1);

            var allRooms = _roomService.GetAll();
            AvailableRooms = allRooms.Count(r => r.RoomStatus == 1);
            BookedRooms = allRooms.Count(r => r.RoomStatus == 0);

            TotalRevenue = _bookingService.GetAllBookings()
                                      .Where(b => b.BookingStatus == 0)
                                      .Sum(b => b.TotalPrice);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}