using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; } = string.Empty;
        public string TypeDescription { get; set; } = string.Empty;
        public string TypeNote { get; set; } = string.Empty;
    }
}
