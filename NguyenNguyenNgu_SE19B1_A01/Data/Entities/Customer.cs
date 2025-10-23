using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerFullName { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime CustomerBirthday { get; set; }
        public byte CustomerStatus { get; set; } = 1; // 1: Active, 2: Deleted
        public string Password { get; set; } = string.Empty;
    }
}
