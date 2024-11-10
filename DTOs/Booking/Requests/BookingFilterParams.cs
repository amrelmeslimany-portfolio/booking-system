using api.Config.Enums;

namespace api.DTOs.Booking.Requests;

public record BookingFilterParams
{
    public BookingStatus? Status { get; set; } = null;
    public DateTime? CheckIn { get; set; } = null;
    public DateTime? CheckOut { get; set; } = null;
}
