using System;

namespace api.Models.Booking;

public class BookingJobModel
{
    public Guid Id { get; set; }
    public required string IdCheckInJob { get; set; }
    public required string IdCheckOutJob { get; set; }
    public Guid BookingId { get; set; }
    public BookingModel? Booking { get; set; }
}
