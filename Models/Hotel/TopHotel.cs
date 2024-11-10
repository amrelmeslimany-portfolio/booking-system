namespace api.Models.Hotel;

public class TopHotel
{
    public required HotelModel Hotel { get; set; }
    public int BookingsCount { get; set; }
}
