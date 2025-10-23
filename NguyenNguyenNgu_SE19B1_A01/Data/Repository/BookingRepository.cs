using Data.Database;
using Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository
{
    public class BookingRepository
    {
        private readonly InMemoryDatabase _db = InMemoryDatabase.Instance;
        public IEnumerable<BookingReservation> GetAll() => _db.Bookings;

    }
}