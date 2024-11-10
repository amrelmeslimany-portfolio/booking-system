namespace api.Models.Hotel
{
    public class LocationModel
    {
        public Guid Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int? PostalCode { get; set; }
        public GeoCoordinates? GeoCoordinates { get; set; }
        public Guid HotelId { get; set; }
        public HotelModel Hotel { get; set; } = null!;
    }
}
