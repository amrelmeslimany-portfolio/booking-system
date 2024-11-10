using System.ComponentModel.DataAnnotations;
using api.Config.Enums;
using api.Models.Hotel;

namespace api.DTOs.Hotel.Requests;

public class CreateHotelRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(10)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [RegularExpression(
        @"^\+\d{12,18}$",
        ErrorMessage = "Must be start with (+) and length range is 12 - 18 number"
    )]
    public string PhoneNumber { get; set; } = string.Empty;
    public CreateLocationRequest? Location { get; set; }
    public List<CreateImageGalleryRequest>? Gellary { get; set; }
    public HotelStatus? Status { get; set; } = HotelStatus.Open;

    [Required]
    public required HotelServices Services { get; set; }

    [EmailAddress]
    public string? EmailContact { get; set; }

    [Url]
    public string? WebsiteURL { get; set; }
}
