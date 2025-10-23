using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Database
{
    public sealed class InMemoryDatabase
    {
        private static readonly Lazy<InMemoryDatabase> _instance = new(() => new InMemoryDatabase());
        public static InMemoryDatabase Instance => _instance.Value;

        public List<Customer> Customers { get; set; }
        public List<RoomType> RoomTypes { get; set; }
        public List<RoomInformation> Rooms { get; set; }
        public List<BookingReservation> Bookings { get; set; }
        public List<BookingDetail> BookingDetails { get; set; }

        private InMemoryDatabase()
        {

            RoomTypes = new List<RoomType>
            {
                new() { RoomTypeID = 1, RoomTypeName = "Standard room", TypeDescription = "This is typically the most affordable option and provides basic amenities such as a bed, dresser, and TV.", TypeNote = "N/A" },
                new() { RoomTypeID = 2, RoomTypeName = "Suite", TypeDescription = "Suites usually offer more space and amenities than standard rooms, such as a separate living area, kitchenette, and multiple bathrooms.", TypeNote = "N/A" },
                new() { RoomTypeID = 3, RoomTypeName = "Deluxe room", TypeDescription = "Deluxe rooms offer additional features such as a balcony or sea view, upgraded bedding, and improved décor.", TypeNote = "N/A" },
                new() { RoomTypeID = 4, RoomTypeName = "Executive room", TypeDescription = "Executive rooms are designed for business travelers and offer perks such as free breakfast, evening drink, and high-speed internet.", TypeNote = "N/A" },
                new() { RoomTypeID = 5, RoomTypeName = "Family Room", TypeDescription = "A room specifically designed to accommodate families, often with multiple beds and additional space for children.", TypeNote = "N/A" },
                new() { RoomTypeID = 6, RoomTypeName = "Connecting Room", TypeDescription = "Two or more rooms with a connecting door, providing flexibility for larger groups or families traveling together.", TypeNote = "N/A" },
                new() { RoomTypeID = 7, RoomTypeName = "Penthouse Suite", TypeDescription = "An extravagant, top-floor suite with exceptional views and exclusive amenities, typically chosen for special occasions or VIP guests.", TypeNote = "N/A" },
                new() { RoomTypeID = 8, RoomTypeName = "Bungalow", TypeDescription = "A standalone cottage-style accommodation, providing privacy and a sense of seclusion often within a resort setting", TypeNote = "N/A" }
            };
            Customers = new List<Customer>
            {
                new() { CustomerID = 3, CustomerFullName = "William Shakespeare", Telephone = "0903939393", EmailAddress = "WilliamShakespeare@FUMiniHotel.org", CustomerBirthday = new DateTime(1990, 2, 2), CustomerStatus = 1, Password = "123@" },
                new() { CustomerID = 5, CustomerFullName = "Elizabeth Taylor", Telephone = "0903939377", EmailAddress = "ElizabethTaylor@FUMiniHotel.org", CustomerBirthday = new DateTime(1991, 3, 3), CustomerStatus = 1, Password = "144@" },
                new() { CustomerID = 8, CustomerFullName = "James Cameron", Telephone = "0903946582", EmailAddress = "JamesCameron@FUMiniHotel.org", CustomerBirthday = new DateTime(1992, 11, 10), CustomerStatus = 1, Password = "443@" },
                new() { CustomerID = 9, CustomerFullName = "Charles Dickens", Telephone = "0903955633", EmailAddress = "CharlesDickens@FUMiniHotel.org", CustomerBirthday = new DateTime(1991, 12, 5), CustomerStatus = 1, Password = "563@" },
                new() { CustomerID = 10, CustomerFullName = "George Orwell", Telephone = "0913933493", EmailAddress = "GeorgeOrwell@FUMiniHotel.org", CustomerBirthday = new DateTime(1993, 12, 24), CustomerStatus = 1, Password = "177@" },
                new() { CustomerID = 11, CustomerFullName = "Victoria Beckham", Telephone = "0983246773", EmailAddress = "VictoriaBeckham@FUMiniHotel.org", CustomerBirthday = new DateTime(1990, 9, 9), CustomerStatus = 1, Password = "654@" },
                new() { CustomerID = 12, CustomerFullName = "System Administrator", Telephone = "0000000000", EmailAddress = "admin@FUMiniHotelSystem.com", CustomerBirthday = new DateTime(1990, 1, 1), CustomerStatus = 1, Password = "@@abc123@@" } // Admin
            };

            Rooms = new List<RoomInformation>
            {
                new() { RoomID = 1, RoomNumber = "2364", RoomDetailDescription = "A basic room with essential amenities, suitable for individual travelers or couples.", RoomMaxCapacity = 3, RoomTypeID = 1, RoomStatus = 1, RoomPricePerDay = 149.0000m },
                new() { RoomID = 2, RoomNumber = "3345", RoomDetailDescription = "Deluxe rooms offer additional features such as a balcony or sea view, upgraded bedding, and improved décor.", RoomMaxCapacity = 5, RoomTypeID = 3, RoomStatus = 1, RoomPricePerDay = 299.0000m },
                new() { RoomID = 3, RoomNumber = "4432", RoomDetailDescription = "A luxurious and spacious room with separate living and sleeping areas, ideal for guests seeking extra comfort and space.", RoomMaxCapacity = 4, RoomTypeID = 2, RoomStatus = 0, RoomPricePerDay = 199.0000m }, // Đã đặt bởi Booking 1 và 2
                new() { RoomID = 5, RoomNumber = "3342", RoomDetailDescription = "Foor 3, Window in the North West ", RoomMaxCapacity = 5, RoomTypeID = 5, RoomStatus = 0, RoomPricePerDay = 219.0000m }, // Đã đặt bởi Booking 2
                new() { RoomID = 7, RoomNumber = "4434", RoomDetailDescription = "Floor 4, main window in the South ", RoomMaxCapacity = 4, RoomTypeID = 1, RoomStatus = 0, RoomPricePerDay = 179.0000m } // Đã đặt bởi Booking 1
            };

            Bookings = new List<BookingReservation>
            {
                new() { BookingReservationID = 1, BookingDate = new DateTime(2023, 12, 20), TotalPrice = 378.0000m, CustomerID = 3, BookingStatus = 1 },
                new() { BookingReservationID = 2, BookingDate = new DateTime(2023, 12, 21), TotalPrice = 1493.0000m, CustomerID = 3, BookingStatus = 1 }
            };
            BookingDetails = new List<BookingDetail>
            {
                new() { BookingReservationID = 1, RoomID = 3, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 2), ActualPrice = 199.0000m },
                new() { BookingReservationID = 1, RoomID = 7, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 2), ActualPrice = 179.0000m },
                new() { BookingReservationID = 2, RoomID = 3, StartDate = new DateTime(2024, 1, 5), EndDate = new DateTime(2024, 1, 6), ActualPrice = 199.0000m },
                new() { BookingReservationID = 2, RoomID = 5, StartDate = new DateTime(2024, 1, 5), EndDate = new DateTime(2024, 1, 9), ActualPrice = 219.0000m } 
            };

            UpdateRoomStatusBasedOnBookings();
        }
        private void UpdateRoomStatusBasedOnBookings()
        {
            DateTime today = DateTime.Now.Date;
            var activeBookingRoomIds = BookingDetails
                .Where(bd => bd.StartDate.Date <= today && bd.EndDate.Date > today) 
                .Select(bd => bd.RoomID)
                .Distinct()
                .ToList();

            foreach (var room in Rooms)
            {
                if (activeBookingRoomIds.Contains(room.RoomID))
                {
                    room.RoomStatus = 0; 
                }
                else
                {
                    room.RoomStatus = 1; 
                }
            }
        }
    }
}