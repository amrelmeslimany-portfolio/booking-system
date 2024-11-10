using api.Config.Enums;
using api.DTOs.Hotel.Responses;
using api.Models.Common;

namespace api.DTOs.Booking.Responses
{
    public class BookingResponse
    {
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class BookingCustomerResponse : BookingResponse
    {
        public decimal TotalPrice { get; set; }
        public required string HotelName { get; set; }
    }

    public class BookingOwnerResponse : BookingResponse
    {
        public required OwnerResponse Customer { get; set; }
    }
}
