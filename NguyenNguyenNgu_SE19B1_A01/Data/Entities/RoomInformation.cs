using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class RoomInformation
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomDetailDescription { get; set; } = string.Empty;
        public int RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        public byte RoomStatus { get; set; } = 1;
        public decimal RoomPricePerDay { get; set; }
    }
}
