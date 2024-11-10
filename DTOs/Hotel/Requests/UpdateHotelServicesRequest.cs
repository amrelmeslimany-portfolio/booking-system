namespace api.DTOs.Hotel.Requests;

public class UpdateHotelServicesRequest
{
    public bool? HasWifi { get; set; }
    public bool? HasPool { get; set; }
    public bool? HasGYM { get; set; }
    public bool? HasRestaurant { get; set; }
    public bool? HasParking { get; set; }
}
