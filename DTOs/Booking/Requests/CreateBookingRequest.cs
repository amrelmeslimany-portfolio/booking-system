using System.ComponentModel.DataAnnotations;
using api.Config.Filters;

namespace api.DTOs.Booking.Requests;

public class CreateBookingRequest
{
    [Required]
    [ValidateDateTense]
    public DateTime CheckInDate { get; set; }

    [Required]
    [ValidateDateRange(
        "CheckInDate",
        ErrorMessage = "CheckOutDate must be greater than CheckInDate"
    )]
    [ValidateDateTense]
    public DateTime CheckOutDate { get; set; }

    [Required]
    [Range(1, 10)]
    public int NumberOfGuests { get; set; }
}
