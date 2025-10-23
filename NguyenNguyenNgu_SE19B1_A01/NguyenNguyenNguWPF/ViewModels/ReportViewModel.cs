using Business.Services;
using Data.Entities;
using NguyenNguyenNguWPF.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace NguyenNguyenNguWPF.ViewModels
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        private readonly BookingService _bookingService = new();
        private List<BookingReservation> _allBookings;

        private ICollectionView? _bookingsView;
        private decimal _totalRevenue;
        private int _filteredBookingCount;

        private DateTime _startDate = new DateTime(2023, 1, 1); 

        private DateTime _endDate = DateTime.Now.Date; 
        private string _selectedSortProperty = "BookingDate";
        private ListSortDirection _selectedSortDirection = ListSortDirection.Descending;

        public DateTime StartDate {  get => _startDate; set { _startDate = value.Date; OnPropertyChanged(); } }
        public DateTime EndDate { get => _endDate; set { _endDate = value.Date; OnPropertyChanged(); } }
        public ICollectionView? BookingsView {  get => _bookingsView; set { _bookingsView = value; OnPropertyChanged(); } }
        public decimal TotalRevenue {  get => _totalRevenue; set { _totalRevenue = value; OnPropertyChanged(); } }
        public int FilteredBookingCount {  get => _filteredBookingCount; set { _filteredBookingCount = value; OnPropertyChanged(); } }
        public List<string> SortProperties { get; } = new List<string> { "BookingDate", "TotalPrice" };
        public string SelectedSortProperty {  get => _selectedSortProperty; set { _selectedSortProperty = value; OnPropertyChanged(); } }
        public ICommand GenerateCommand { get; }

        public ReportViewModel()
        {
            _allBookings = _bookingService.GetAllBookings().ToList();
            GenerateCommand = new RelayCommand(GenerateReport);
            GenerateReport(null);
        }

        private void GenerateReport(object? obj = null)
        {
            var startDate = this.StartDate;
            var endDate = this.EndDate.AddDays(1); 
            var filteredBookings = _allBookings.Where(b =>
                b.BookingDate >= startDate &&
                b.BookingDate < endDate
            ).ToList();

            UpdateStats(filteredBookings);
            BookingsView = CollectionViewSource.GetDefaultView(filteredBookings);
            ApplySorting();
        }

        private void ApplySorting()
        { 
            if (BookingsView == null) return;
            BookingsView.SortDescriptions.Clear();
            _selectedSortDirection = ListSortDirection.Descending;
            BookingsView.SortDescriptions.Add(new SortDescription(SelectedSortProperty, _selectedSortDirection));
            BookingsView.Refresh();
        }

        private void UpdateStats(List<BookingReservation> bookings)
        {
            TotalRevenue = bookings.Where(b => b.BookingStatus == 0).Sum(b => b.TotalPrice);
            FilteredBookingCount = bookings.Count;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}