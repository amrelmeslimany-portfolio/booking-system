using api.Config.Enums;

namespace api.DTOs.Room.Responses
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public decimal PricePerNight { get; set; }
        public RoomTypes? Type { get; set; }
        public RoomStatus? Status { get; set; }
    }
}
