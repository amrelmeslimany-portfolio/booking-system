using System;

namespace api.DTOs.Hotel.Responses;

public class TopHotelResponse
{
    public required HotelListResponse Hotel { get; set; }
    public int BookingsCount { get; set; }
}
