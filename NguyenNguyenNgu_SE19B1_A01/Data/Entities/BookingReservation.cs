using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class BookingReservation
    {
        public int BookingReservationID { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustomerID { get; set; }
        public byte BookingStatus { get; set; } = 1; // 1: Active, 2: Completed, 3: Cancelled
    }
}
