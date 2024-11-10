using System;
using System.Text.Json.Serialization;
using api.DTOs.Hotel.Responses;

namespace api.DTOs.Booking.Responses;

public class HotelBookingResponse : HotelListResponse
{
    [JsonIgnore]
    public override List<GellaryResponse>? Gellary
    {
        get => base.Gellary;
        set => base.Gellary = value;
    }
}
