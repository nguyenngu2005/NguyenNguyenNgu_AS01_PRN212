using Business.Interfaces;
using Data.Entities;
using Data.Database;

namespace Business.Services
{
    public class ReportService : IReportService
    {
        private readonly InMemoryDatabase _db = InMemoryDatabase.Instance;

        public IEnumerable<BookingReservation> GetReport(DateTime start, DateTime end)
        {
            return _db.Bookings
                .Where(b => b.BookingDate >= start && b.BookingDate <= end)
                .OrderByDescending(b => b.BookingDate);
        }

        public decimal GetTotalRevenue(DateTime start, DateTime end)
        {
            return _db.BookingDetails
                .Where(d => d.StartDate >= start && d.EndDate <= end)
                .Sum(d => d.ActualPrice);
        }
    }
}
