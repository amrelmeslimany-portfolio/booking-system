using System;
using api.Config.Enums;
using api.Models.Booking;
using api.Models.Common;
using api.Models.Hotel;

namespace api.Models.Room
{
    public class RoomModel : BaseModel
    {
        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid HotelId { get; set; }
        public required HotelModel Hotel { get; set; }
        public List<BookingModel> Bookings { get; set; } = [];
        public RoomTypes? Type { get; set; } = RoomTypes.Single;
        public RoomStatus? Status { get; set; } = RoomStatus.Available;
    }
}
