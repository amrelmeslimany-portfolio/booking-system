using System;
using api.Models.Common;

namespace api.Models.Booking;

public class BookingJobModel : BaseModel
{
    public required string IdCheckInJob { get; set; }
    public required string IdCheckOutJob { get; set; }
    public Guid BookingId { get; set; }
    public BookingModel? Booking { get; set; }
}
