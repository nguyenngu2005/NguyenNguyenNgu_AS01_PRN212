using Data.Entities;
using Data.Repository;
using System.Collections.Generic;

namespace Business.Services
{
    public class BookingService
    {
        private readonly BookingRepository _repo = new();
        public IEnumerable<BookingReservation> GetAllBookings()
        {
            return _repo.GetAll();
        }
    }
}