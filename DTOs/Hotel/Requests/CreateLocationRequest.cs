using System.ComponentModel.DataAnnotations;
using api.Models.Hotel;

namespace api.DTOs.Hotel.Requests;

public class CreateLocationRequest
{
    [Required]
    [MaxLength(50)]
    public string City { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string State { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Country { get; set; } = string.Empty;

    [RegularExpression(@"^\d{5}$", ErrorMessage = "Must be 5 numbers")]
    public int? PostalCode { get; set; }
    public GeoCoordinates? GeoCoordinates { get; set; }
}
