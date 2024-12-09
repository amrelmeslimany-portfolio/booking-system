using api.Models.Common;

namespace api.Models.Hotel
{
    public class LocationModel : BaseModel
    {
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int? PostalCode { get; set; }
        public GeoCoordinates? GeoCoordinates { get; set; }
        public Guid HotelId { get; set; }
        public HotelModel Hotel { get; set; } = null!;
    }
}
