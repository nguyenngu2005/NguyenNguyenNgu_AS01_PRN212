using Data.Entities;

namespace Business.Interfaces
{
    public interface IReportService
    {
        IEnumerable<BookingReservation> GetReport(DateTime start, DateTime end);
        decimal GetTotalRevenue(DateTime start, DateTime end);
    }
}
