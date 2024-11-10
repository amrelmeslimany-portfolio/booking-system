using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Hotel.Requests;

public class UpdateLocationRequest
{
    [MaxLength(50)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? State { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; }

    [RegularExpression(@"^\d{5}$", ErrorMessage = "Must be 5 numbers")]
    public int? PostalCode { get; set; }

    public UpdateGeoCoordinatesRequest? GeoCoordinates { get; set; }
}

public class UpdateGeoCoordinatesRequest
{
    public double? Latitude { get; set; } = null;
    public double? Longitude { get; set; } = null;
}
