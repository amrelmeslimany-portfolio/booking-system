using api.Config.Enums;
using api.Models.Common;
using api.Models.Room;
using api.Models.User;

namespace api.Models.Booking
{
    public class BookingModel : BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public Guid RoomId { get; set; }
        public required RoomModel Room { get; set; }
        public required string CustomerId { get; set; }
        public required AppUserModel Customer { get; set; }
        public BookingJobModel? Job { get; set; }
    }
}
