using System;
using api.Models.Booking;
using api.Models.Hotel;
using Microsoft.AspNetCore.Identity;

namespace api.Models.User
{
    public class AppUserModel : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Picture { get; set; }
        public HotelModel? Hotel { get; set; }
        public List<BookingModel>? Bookings { get; set; }
    }
}
