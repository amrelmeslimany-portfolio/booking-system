using System;
using api.Models.Hotel;

namespace api.DTOs.Hotel.Responses
{
    public class LocationResponse
    {
        public Guid Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int? PostalCode { get; set; }
        public GeoCoordinates? GeoCoordinates { get; set; }
    }
}
