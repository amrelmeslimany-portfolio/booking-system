using api.Config.Enums;
using api.DTOs.Hotel.Responses;
using api.Models.Common;

namespace api.DTOs.Room.Responses
{
    public class RoomDetailsResponse : BaseModel
    {
        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public int TotalBookings { get; set; }
        public string Description { get; set; } = string.Empty;
        public required HotelBriefResponse Hotel { get; set; }
        public RoomTypes Type { get; set; }
        public RoomStatus Status { get; set; }
    }
}
