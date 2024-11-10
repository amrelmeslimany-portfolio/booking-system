using api.Config.Enums;
using api.DTOs.Hotel.Responses;
using api.DTOs.Room.Responses;
using api.Models.Room;

namespace api.DTOs.Booking.Responses;

public class BookingDetailsResponse
{
    public Guid Id { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public required RoomResponse Room { get; set; }
    public required HotelBookingResponse Hotel { get; set; }
    public required OwnerResponse Customer { get; set; }
}
